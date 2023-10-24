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
        [Test]
        public async Task GetPlayerQuests_Test()
        {
            var result = await _client.GetPlayerQuests("Mariapori");
            Assert.IsTrue(result.Any());
        }
        [Test]
        public async Task GetPlayerQuests_Test_2()
        {
            // Check difficulty filter works
            var result = await _client.GetPlayerQuests("Mariapori", Difficulty.Novice);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Novice),Is.True);

            result = await _client.GetPlayerQuests("Mariapori", Difficulty.Experienced);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Experienced), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", Difficulty.Intermediate);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Intermediate), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", Difficulty.Master);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Master), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", Difficulty.Grandmaster);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Grandmaster), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", Difficulty.Special);
            Assert.That(result.All(o => o.Difficulty == Difficulty.Special), Is.True);
        }
        [Test]
        public async Task GetPlayerQuests_Test_3()
        {
            // Check status filter works
            var result = await _client.GetPlayerQuests("Mariapori",filterStatus: QuestStatus.NOT_STARTED);
            Assert.That(result.All(o => o.Status.ToLower().Contains(QuestStatus.NOT_STARTED.ToString().ToLower())), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", filterStatus: QuestStatus.STARTED);
            Assert.That(result.All(o => o.Status.ToLower().Contains(QuestStatus.STARTED.ToString().ToLower())), Is.True);

            result = await _client.GetPlayerQuests("Mariapori", filterStatus: QuestStatus.COMPLETED);
            Assert.That(result.All(o => o.Status.ToLower().Contains(QuestStatus.COMPLETED.ToString().ToLower())), Is.True);
        }
        [Test]
        public async Task GetOnlinePlayerCount_Test()
        {
            // Checking only are result != 0
            var result = await _client.GetOnlinePlayerCount();
            Assert.That(result, Is.Not.EqualTo(0));
        }
        [Test]
        public async Task GetMonthlyXp_Test()
        {
            var result = await _client.GetMonthlyXp("Mariapori");
            Assert.That(result,Is.Not.Null);
        }
    }
}
