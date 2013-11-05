//=========================================================================
/*
 * Trick from Enterprise Library for .NET 2.0 used to write unit tests for both 
 * NUnit and VS UT
*/
# define NUNIT
//# define NTIME

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif
#if NTIME
	using NTime.Framework;
# else
//using TimerDurationTest = global::Core.DLL_00Development.TestAttribute;
# endif
//=========================================================================

using System;
using System.Text;
using System.Collections.Generic;

using System.Diagnostics;


namespace UnitTests.PoorMansEditor.Common
{
	/// <summary>
	/// Summary description for Task
	/// </summary>
	[TestClass]
	public
	  partial
	  class
	  TestPoorMansEditorTextParser
#if SILVERLIGHT
	  : SilverlightTest
#endif
	{
		string text = @"
.large
My First Document
.normal
.paragraph
This is my 
.italics
very first
.regular
document, and I am very proud that I am getting this on the string.   While this paragraph is not filled, the following one has automatic filling set:
.paragraph
.indent +2
.fill
Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
.nofill
.indent -2
Well, that was 
.bold
exciting
.regular
good luck!
";

		/// <summary>
		/// Constructor default
		/// </summary>
		public
		   TestPoorMansEditorTextParser
		  (
		  )
		{
			return;
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code_class before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code_class after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code_class before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code_class after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		/// <summary>
		/// Swap test case
		/// </summary>
		[TestMethod]  //[TestMethod]
		public
		  void
		  TestConversion1
		  (
		  )
		{
			global::PoorMansEditor.Common.TextParser parser = null;
			parser = new global::PoorMansEditor.Common.TextParser();

			parser.PoorManEditorText2Html(text);

			return;
		}

		[TestMethod]  //[TestMethod]
		public
		  void
		  TestConversion1
		  (
		  )
		{
			global::PoorMansEditor.Common.TextParser parser = null;
			parser = new global::PoorMansEditor.Common.TextParser();

			parser.PoorManEditorText2Html(text + Environment.NewLine + text );

			return;
		}


		[TestMethod]  //[TestMethod]
		public
		  void
		  TestConversion1
		  (
		  )
		{
			global::PoorMansEditor.Common.TextParser parser = null;
			parser = new global::PoorMansEditor.Common.TextParser();

			parser.PoorManEditorText2Html(text + Environment.NewLine + text + Environment.NewLine + text);

			return;
		}
	}
}
