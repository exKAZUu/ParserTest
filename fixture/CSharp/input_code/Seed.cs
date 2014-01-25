using System;

namespace Occf.Learner.Core.Tests.Experiments {
	internal class Seed {
		public static void Main(string[] args) {
			Contract.Requires(true);
			Contract.Requires(true, "msg");
			Contract.Requires<Exception>(true);
			Contract.Requires<Exception>(true, "msg");

			System.Diagnostics.Contracts.Contract.Requires(true);
			System.Diagnostics.Contracts.Contract.Requires(true, "msg");
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true);
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true, "msg");
			
			TEST:
			for (int i = 0; i < args.Length; i++) {
			}
			TEST2:
			while (args.Length == 0) {
			}
			TEST3:
			do {
			} while (args.Length == 0);
			if (args.Length < 0) {
				Console.WriteLine();
				;
				{ Console.WriteLine(); }
			}
			;
		}
	}
}
