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
using LibGit2Sharp;

namespace ParserTests {
    public static class Git {
        #region Process

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

        #endregion

        #region Deletion

        public static void ForceDeleteDirectory(string dirPath) {
            var dirInfo = new DirectoryInfo(dirPath);
            ForceDeleteDirectory(dirInfo);
        }

        public static void ForceDeleteDirectory(DirectoryInfo dirInfo) {
            RemoveReadonlyAttribute(dirInfo);
            dirInfo.Delete(true);
        }

        private static void RemoveReadonlyAttribute(DirectoryInfo dirInfo) {
            if ((dirInfo.Attributes & FileAttributes.ReadOnly) ==
                FileAttributes.ReadOnly) {
                dirInfo.Attributes = FileAttributes.Normal;
            }
            foreach (var fi in dirInfo.EnumerateFiles()) {
                if ((fi.Attributes & FileAttributes.ReadOnly) ==
                    FileAttributes.ReadOnly) {
                    fi.Attributes = FileAttributes.Normal;
                }
            }
            foreach (var di in dirInfo.EnumerateDirectories()) {
                RemoveReadonlyAttribute(di);
            }
        }

        #endregion

        public static string CloneAndCheckout(string repoPath, string url, string commitPointer) {
            Clone(repoPath, url);
            return Checkout(repoPath, commitPointer);
        }

        public static string CloneAndCheckoutAndReset(
                string repoPath, string url, string commitPointer) {
            var cloned = Clone(repoPath, url);
            var ret = Checkout(repoPath, commitPointer);
            if (!cloned) {
                Reset(repoPath);
            }
            return ret;
        }

        public static bool Clone(string repoPath, string url) {
            if (!PrepareDirectoryToClone(repoPath)) {
                return false;
            }
            var workPath = Path.GetDirectoryName(repoPath);
            Console.Write("Cloning " + url);
            InvokeProcess("git", new[] { "clone", url }, workPath);
            Console.WriteLine(" done");
            return true;
        }

        public static string Checkout(string repoPath, string commitPointer) {
            using (var repo = new Repository(repoPath)) {
                if (repo.Commits.First().Sha.StartsWith(commitPointer)) {
                    return repo.Commits.First().Sha;
                }
            }
            InvokeProcess("git", new[] { "fetch", "origin" }, repoPath);
            InvokeProcess("git", new[] { "checkout", commitPointer }, repoPath);
            using (var repo = new Repository(repoPath)) {
                return repo.Commits.First().Sha;
            }
        }

        public static void Reset(string repoPath) {
            Console.Write("Resetting ...");
            InvokeProcess("git", new[] { "reset", "--hard" }, repoPath);
            Console.WriteLine(" done");
        }

        public static bool PrepareDirectoryToClone(string repoPath) {
            if (!Directory.Exists(repoPath)) {
                return true;
            }
            if (Directory.EnumerateFileSystemEntries(repoPath).Skip(1).Any()) {
                using (var repo = new Repository(repoPath)) {
                    if (repo.Commits.Any()) {
                        return false;
                    }
                }
            }
            ForceDeleteDirectory(repoPath);
            return true;
        }
    }
}