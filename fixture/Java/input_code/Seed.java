package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] args) {
		int a = 0;
		int b, c;
		class A { }
		
		checkArgument(args == null);
		checkArgument(args == null, "test");
		Preconditions.checkArgument(args != null);
		Preconditions.checkArgument(args != null, "test");
		com.google.common.base.Preconditions.checkArgument(args.length == 0);
		com.google.common.base.Preconditions.checkArgument(args.length == 0, "test");

		TEST:
		for (i = 0; i < args.length; i++) { }
		while (args.length == 0) { }
		do { } while (args.length == 0);
		if (args.length < 0) {
			TEST4:
			System.out.println(0 + 1 * 2 / 3 % 4);
			;
			{ System.out.println(); }
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
		
		checkArgument(true);
		checkArgument(true, "test");
		Preconditions.checkArgument(true);
		Preconditions.checkArgument(true, "test");
		com.google.common.base.Preconditions.checkArgument(true);
		com.google.common.base.Preconditions.checkArgument(true, "test");

		TEST:
		for (; true;) { }
		while (true) { }
		do { } while (true);
		if (true) { } else if (true) { } else { }
	}
}
