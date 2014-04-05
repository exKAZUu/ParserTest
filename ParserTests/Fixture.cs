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
using System.IO;
using System.Linq;

namespace ParserTests {
    public static class Fixture {
        private static string _fixturePath;

        public static string FixturePath {
            get { return _fixturePath ?? (_fixturePath = GetPath()); }
        }

        private static string FindParserTestsSolutionFile(string dirPath) {
            var ret = Directory.GetFiles(dirPath, "ParserTests.sln", SearchOption.AllDirectories);
            return ret.Length == 0 ? null : ret[0];
        }

        private static string GetPath() {
            var path = Environment.CurrentDirectory;
            string solutionPath;
            while ((solutionPath = FindParserTestsSolutionFile(path)) == null) {
                path = Path.GetDirectoryName(path);
                if (path == null) {
                    throw new IOException("'fixture' directory is not found.");
                }
            }
            return Path.Combine(Path.GetDirectoryName(solutionPath), "fixture");
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