using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visualbean.Pokemon.Pokemon;

namespace Visualbean.Pokemon.UnitTest
{
    [TestClass]
    public class LanguageTests
    {
        [TestMethod]
        public void Language_WithConversion_AreEqual()
        {
            var languageString = "en";
            var language = new Language(languageString);

            Assert.IsTrue(language == languageString, "==");
            Assert.IsTrue(language.Equals(languageString), "Equality with implicit conversion");
            Assert.AreEqual(language, new Language(languageString), "Equality");
            Assert.AreNotEqual(language, null);
            Assert.AreNotEqual(language, new { });
        }
    }
}
