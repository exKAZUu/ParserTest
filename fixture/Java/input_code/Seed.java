package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] i) {
		checkArgument(b);

		L: i = 0;
		;
		{ i = 0; }
		f(0 + 1 - 2 * 3 / 4 % 5);

		for (; b;) { }
		while (b) { }
		do { } while (b);
		if (b) { } else if (b) { } else { }

		switch (b) {
			case 0:
			case 1:
				break;
			default:
				break;
		}

		checkArgument(true);

		for (; true;) { }
		while (true) { }
		do { } while (true);
		if (true) { } else if (true) { } else { }
	}
}
