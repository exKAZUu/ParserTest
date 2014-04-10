﻿#region License

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
using System.Linq;
using LibGit2Sharp;

namespace ParserTests {
    public static class Git {
        public static string Clone(string url, string commitPointer, string outPath) {
            Console.Write("Cloning ...");
            var clonedRepoPath = Repository.Clone(url, outPath);
            Console.WriteLine(" done");
            Checkout(clonedRepoPath, commitPointer);
            return clonedRepoPath;
        }

        public static string Checkout(string repoPath, string commitPointer) {
            using (var repo = new Repository(repoPath)) {
                if (!repo.Commits.Any() || !repo.Commits.First().Sha.StartsWith(commitPointer)) {
                    repo.RemoveUntrackedFiles();
                    repo.Reset(ResetMode.Hard);
                    repo.Checkout(commitPointer);
                }
            }
            return repoPath;
        }
    }
}