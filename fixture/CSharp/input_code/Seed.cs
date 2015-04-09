using System;

namespace Occf.Learner.Core.Ts.Experiments {
	internal class Seed {
		public static void Main(string[] a) {
			Contract.Requires(b);
			Contract.Requires(b, "");
			Contract.Requires<Exception>(b);
			Contract.Requires<Exception>(b, "");

			System.Diagnostics.Contracts.Contract.Requires(b);
			System.Diagnostics.Contracts.Contract.Requires(b, "");
			System.Diagnostics.Contracts.Contract.Requires<Exception>(b);
			System.Diagnostics.Contracts.Contract.Requires<Exception>(b, "");

			T:
			f(0 + 1 - 2 * 3 / 4 % 5);
			;
			{ f(); }

			for (; b;) { }
			while (b) { }
			do { } while (b);
			if (b) { } else if (b) { } else { }

			switch (1) {
				case 0:
				case 1:
					break;
				default:
					break;
			}

			Contract.Requires(true);
			Contract.Requires(true, "");
			Contract.Requires<Exception>(true);
			Contract.Requires<Exception>(true, "");

			System.Diagnostics.Contracts.Contract.Requires(true);
			System.Diagnostics.Contracts.Contract.Requires(true, "");
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true);
			System.Diagnostics.Contracts.Contract.Requires<Exception>(true, "");

			for (; true;) { }
			while (true) { }
			do { } while (true);
			if (true) { } else if (true) { } else { }
		}
	}
}
