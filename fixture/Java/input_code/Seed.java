package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] args) {
		int a = 0;
		int b, c;
		class A {
		}
		
		checkArgument(args == null);
		checkArgument(args == null, "test");
		Preconditions.checkArgument(args != null);
		Preconditions.checkArgument(args != null, "test");
		com.google.common.base.Preconditions.checkArgument(args.length == 0);
		com.google.common.base.Preconditions.checkArgument(args.length == 0, "test");

		TEST:
		for (i = 0; i < args.length; i++) {
		}
		TEST2:
		while (args.length == 0) {
		}
		TEST3:
		do {
		} while (args.length == 0);
		if (args.length < 0) {
			TEST4:
			System.out.println();
			;
			{ System.out.println(); }
		}
	}
}
