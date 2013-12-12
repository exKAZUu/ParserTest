package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] args) {
		Preconditions.checkArgument(args != null);
		Preconditions.checkArgument(args.length == 0);

		TEST:
		for (int i = 0; i < args.length; i++) {
			System.out.println();
		}
		TEST2:
		while (args.length == 0) {
			System.out.println();
		}
		TEST3:
		do {
			System.out.println();
		} while (args.length == 0);
		{}
		if (args.length < 0) {
			System.out.println();
			;
			{ System.out.println(); }
		}
		;
	}
}
