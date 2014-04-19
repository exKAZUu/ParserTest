using System;

namespace Occf.Learner.Core.Tests.Experiments {
	internal class Seed {
		public static void Main(string[] args) {
			Contract.Requires(args != null);
			Contract.Requires(args != null, "msg");
			Contract.Requires<Exception>(args != null);
			Contract.Requires<Exception>(args != null, "msg");

			System.Diagnostics.Contracts.Contract.Requires(args != null);
			System.Diagnostics.Contracts.Contract.Requires(args != null, "msg");
			System.Diagnostics.Contracts.Contract.Requires<Exception>(args != null);
			System.Diagnostics.Contracts.Contract.Requires<Exception>(args != null, "msg");
			
			TEST:
			for (int i = 0; i < args.Length; i++) { }
			while (args.Length == 0) { }
			do { } while (args.Length == 0);
			if (args.Length < 0) {
				Console.WriteLine(0 + 1 * 2 / 3 % 4);
				;
				{ Console.WriteLine(); }
			} else if (args.Length < 0) {
			} else {
			}

			switch (1) {
				case 0:
				case 1:
					break;
				default:
					break;
			}

			Contract.Requires(true);
			Contract.Requires(true, "msg");
			Contract.Requires<Exception>(true);
			Contract.Requires<Exception>(true, "msg");

			System.Diagnostics.Contracts.Contract.Requires(true);
			System.Diagnostics.Contracts.Contract.Requires(true, "msg");
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true);
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true, "msg");
			
			TEST:
			for (; true;) { }
			while (true) { }
			do { } while (true);
			if (true) { } else if (true) { } else { }
		}
	}
}
