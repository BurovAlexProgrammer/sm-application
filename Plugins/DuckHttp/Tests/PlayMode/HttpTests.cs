using NUnit.Framework;
using UnityEngine;

namespace Duck.Http.Tests.PlayMode
{
	[TestFixture]
	public class HttpTests
	{
		private Http _battleHttp;

		[OneTimeSetUp]
		public void Setup()
		{
			_battleHttp = Http.Instance;
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(_battleHttp.gameObject);
		}

		[Test]
		public void Expect_Singleton_Is_Created()
		{
			Assert.IsNotNull(_battleHttp);
		}

		[Test]
		public void Expect_Set_Super_Headers()
		{
			const string HEADER_KEY = "HeaderKey";
			const string HEADER_VALUE = "HeaderValue";

			Http.SetSuperHeader(HEADER_KEY, HEADER_VALUE);
			var headers = Http.GetSuperHeaders();

			Assert.IsTrue(headers.ContainsKey(HEADER_KEY));
			Assert.AreEqual(HEADER_VALUE, headers[HEADER_KEY]);
			Assert.AreEqual(1, headers.Count);
		}
	}
}
