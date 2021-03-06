﻿#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
using System.IO;
using System.Linq;
using System.Reflection;

namespace ParserTests {
    public static class Fixture {
        private static string _fixturePath;

        public static string FixturePath =>
                _fixturePath
                ?? (_fixturePath = Path.Combine(GetRootPath("ParserTests.sln"), "fixture"));

        private static string FindParserTestsSolutionFile(string fileNameInRootDir, string dirPath) {
            var ret = Directory.GetFiles(dirPath, fileNameInRootDir, SearchOption.AllDirectories);
            return ret.Length == 0 ? null : ret[0];
        }

        public static string GetRootPath(string fileNameInRootDir) {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string solutionPath;
            do {
                path = Path.GetDirectoryName(path);
                if (path == null) {
                    throw new IOException(fileNameInRootDir + " is not found.");
                }
            } while ((solutionPath = FindParserTestsSolutionFile(fileNameInRootDir, path)) == null);
            return Path.GetDirectoryName(solutionPath);
        }

        private static string GetPath(params string[] subNames) {
            return Path.GetFullPath(subNames.Aggregate(FixturePath, Path.Combine));
        }

        private static string GetPath(string language, string type, IEnumerable<string> subNames) {
            return Path.GetFullPath(
                subNames.Aggregate(Path.Combine(FixturePath, language, type), Path.Combine));
        }

        public static FileInfo GetFile(params string[] subNames) {
            return new FileInfo(GetPath(subNames));
        }

        public static DirectoryInfo GetDirectory(params string[] subNames) {
            return new DirectoryInfo(GetPath(subNames));
        }

        public static string InitializeDirectory(params string[] subNames) {
            var path = CleanUpDirectory(subNames);
            Directory.CreateDirectory(path);
            return path;
        }

        public static string CleanUpDirectory(params string[] subNames) {
            var path = Path.GetFullPath(subNames.Aggregate(FixturePath, Path.Combine));
            if (Directory.Exists(path)) {
                var dirPaths = Directory.EnumerateDirectories(
                    path, "*", SearchOption.TopDirectoryOnly);
                foreach (var dirPath in dirPaths) {
                    Directory.Delete(dirPath, true);
                }
                var filePaths = Directory.EnumerateFiles(
                    path, "*", SearchOption.TopDirectoryOnly);
                foreach (var filePath in filePaths) {
                    File.Delete(filePath);
                }
            }
            return path;
        }

        public static string GetGitRepositoryPath(string url) {
            var names = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (names[names.Length - 1].EndsWith(".git")) {
                names[names.Length - 1] =
                        names[names.Length - 1].Substring(0, names[names.Length - 1].Length - 4);
            }
            var path = GetPath("Git", names[names.Length - 2], names[names.Length - 1]);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

        public static string GetInputCodePath(string languageName, params string[] subNames) {
            return GetPath(languageName, "input_code", subNames);
        }

        public static string GetExpectedXmlPath(string languageName, params string[] subNames) {
            return GetPath(languageName, "xmlexpectation", subNames);
        }

        public static string GetOutputFilePath(params string[] subNames) {
            return subNames.Aggregate(InitializeDirectory("output"), Path.Combine);
        }

        public static string GetOutputDirPath(params string[] subNames) {
            return subNames.Aggregate(InitializeDirectory("output"), Path.Combine);
        }

        public static string GetFailedInputPath(string languageName) {
            var list = new List<string> { languageName, "input_failed_src" };
            return GetPath(list.ToArray());
        }

        public static string GetInputProjectPath(string languageName, params string[] subNames) {
            var list = new List<string> { languageName, "input_project" };
            list.AddRange(subNames);
            return GetPath(list.ToArray());
        }
    }
}