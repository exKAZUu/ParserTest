#region License

// Copyright (C) 2011-2014 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Code2Xml.Core.Generators;
using LibGit2Sharp;

namespace ParserTests {
    public static class Git {
        public static ProcessStartInfo CreateProcessStartInfo(
                string filePath, IEnumerable<string> arguments, string workingDirectory = "") {
            var info = new ProcessStartInfo {
                FileName = filePath,
                Arguments = string.Join(" ", arguments),
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
            };
            return info;
        }

        public static int InvokeProcess(
                string filePath, IEnumerable<string> arguments, string workingDirectory = "") {
            var info = CreateProcessStartInfo(filePath, arguments, workingDirectory);
            using (var p = Process.Start(info)) {
                p.WaitForExit();
                return p.ExitCode;
            }
        }

        public static void CloneAndCheckout(string repoPath, string url, string commitPointer) {
            Directory.CreateDirectory(repoPath);
            if (Directory.GetDirectories(repoPath).Length + Directory.GetFiles(repoPath).Length
                <= 1) {
                Directory.Delete(repoPath, true);
                Directory.CreateDirectory(repoPath);
                Clone(repoPath, url);
            }
            Checkout(repoPath, commitPointer);
        }

        public static void Reset(string repoPath) {
            Console.Write("Resetting ...");
            InvokeProcess("git", new[] { "reset", "--hard" }, repoPath);
            Console.WriteLine(" done");
        }

        public static void Clone(string repoPath, string url) {
            var workPath = Path.GetDirectoryName(repoPath);
            Console.Write("Cloning ...");
            InvokeProcess("git", new[] { "clone", url }, workPath);
            Console.WriteLine(" done");
        }

        public static string Checkout(string repoPath, string commitPointer) {
            using (var repo = new Repository(repoPath)) {
                if (!repo.Commits.Any() || !repo.Commits.First().Sha.StartsWith(commitPointer)) {
                    InvokeProcess("git", new[] { "checkout", commitPointer }, repoPath);
                }
            }
            return repoPath;
        }

        public static Tuple<string, string> GetCommitPointers(
                string repoPath, CstGenerator gen, string searchPattern) {
            using (var repo = new Repository(repoPath)) {
                var now = repo.Commits.First();
                var since = now.Committer.When.AddMonths(-6);
                var commit = repo.Commits.FirstOrDefault(
                        c => {
                            if (c.Committer.When >= since) {
                                return false;
                            }
                            InvokeProcess("git", new[] { "checkout", c.Sha }, repoPath);
                            var files =
                                    Directory.GetFiles(
                                            repoPath, searchPattern, SearchOption.AllDirectories)
                                            .ToList();
                            foreach (var file in files) {
                                try {
                                    gen.GenerateTreeFromCodePath(file, null, true);
                                } catch {
                                    InvokeProcess("git", new[] { "checkout", now.Sha }, repoPath);
                                    return false;
                                }
                            }
                            InvokeProcess("git", new[] { "checkout", now.Sha }, repoPath);
                            return true;
                        })
                             ?? repo.Commits.Last();
                return Tuple.Create(now.Sha, commit.Sha);
            }
        }
    }
}