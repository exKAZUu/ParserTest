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
	}
}