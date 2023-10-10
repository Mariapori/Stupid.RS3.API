namespace Stupid.RS3.API.Tests
{
    public class Tests
    {
        private Client _client;
        [SetUp]
        public void Setup()
        {
            _client = new Client();
        }

        [Test]
        public async Task GetPlayerStatsByName_Test()
        {
            var result = await _client.GetPlayerStatsByName("Mariapori");
            // Check are first stats object overall
            Assert.That(result[0].Skill, Is.EqualTo(Skills.Overall));
            // Check are last stats object necromancy
            Assert.That(result.Last().Skill,Is.EqualTo(Skills.Necromancy));
        }
    }
}
