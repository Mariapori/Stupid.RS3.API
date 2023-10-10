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
            Assert.That(Skills.Overall, Is.EqualTo(result[0].Skill));
            // Check are last stats object necromancy
            Assert.That(Skills.Necromancy,Is.EqualTo(result.Last().Skill));
        }
    }
}