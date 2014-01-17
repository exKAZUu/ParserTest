package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] args) {
		checkArgument(args == null);
		Preconditions.checkArgument(args != null);
		com.google.common.base.Preconditions.checkArgument(args.length == 0, "test");

		TEST:
		for (i = 0; i < args.length; i++) {
			for (; true; ) {
				System.out.println();
			}
		}
		TEST2:
		while (args.length == 0) {
			while (true) {
				System.out.println();
			}
		}
		TEST3:
		do {
			do {
			} while (args.length == 0);
		} while (args.length == 0);
		{}
		if (args.length < 0) {
			if (true) {
			}
			TEST4:
			System.out.println();
			;
			{ System.out.println(); }
		}
		;
	}
}
