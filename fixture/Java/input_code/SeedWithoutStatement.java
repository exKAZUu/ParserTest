package com.google.testing.pogen;

import com.google.common.base.Preconditions;

public class Seed {
	public static void main(String[] args) {
		checkArgument(args == null);
		Preconditions.checkArgument(args != null);
		com.google.common.base.Preconditions.checkArgument(args.length == 0, "test");

		TEST:
		System.out.println();
		TEST2:
		System.out.println();
		TEST3:
		{}
		TEST4:
		System.out.println();
		;
		{ System.out.println(); }
		;
	}
}
