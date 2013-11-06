#region License

// Copyright (C) 2011-2013 Kazunori Sakamoto
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

using System.IO;
using System.Linq;
using System.Reflection;

namespace ParserTest {
	public static class Fixture {
		public static string FixturePath;

		static Fixture() {
			var path = Assembly.GetEntryAssembly().Location;
			do {
				path = Path.GetDirectoryName(path);
				if (path == null) {
					throw new IOException("'fixture' directory is not found.");
				}
			} while (!Directory.Exists(Path.Combine(path, "fixture")));
			FixturePath = Path.Combine(path, "fixture");
		}

		public static string GetFixturePathCleaning(params string[] subNames) {
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
			Directory.CreateDirectory(path);
			return path;
		}

		public static string GetFixtureDirectoryCleaning(params string[] subNames) {
			return GetFixturePathCleaning(subNames);
		}

		public static string GetFixturePath(params string[] subNames) {
			return Path.GetFullPath(subNames.Aggregate(FixturePath, Path.Combine));
		}

		public static FileInfo GetFixtureFile(params string[] subNames) {
			return new FileInfo(GetFixturePath(subNames));
		}

		public static DirectoryInfo GetFixtureDirectory(params string[] subNames) {
			return new DirectoryInfo(GetFixturePath(subNames));
		}

		public static string GetInputPath(string name) {
			return GetFixturePath(name, "input");
		}

		public static string GetXmlExpectationPath(string lang, string relativePath) {
			return GetFixturePath(lang, "xmlexpectation", relativePath);
		}

		public static string GetOutputFilePath(string lang, string relativePath) {
			return Path.Combine(GetFixturePathCleaning("output"), lang, relativePath);
		}
	}
}