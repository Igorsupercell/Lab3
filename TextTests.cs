using Lab3;
using NUnit.Framework;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Lab3.Tests
{
    [TestFixture]
    public class TextTests
    {
        // 1. Тест парсинга текста и подсчета предложений
        [Test]
        public void Parse_ValidInput_CreatesCorrectNumberOfSentences()
        {
            string sampleText = "Привет мир! Как твои дела? Всё отлично.";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var reader = new StreamReader(stream);

            Text text = TextParcer.Parse(reader);

            Assert.That(text.Sentences.Count, Is.EqualTo(3));
        }

        // 2. Тест поиска слов заданной длины в вопросительных предложениях
        [Test]
        public void SenchInQuestionSent_ReturnsUniqueWordsOfGivenLengthFromQuestionsOnly()
        {
            string sampleText = "Это обычное предложение. Что тут происходит? Как дела?";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var reader = new StreamReader(stream);

            Text text = TextParcer.Parse(reader);

            // Ищем слова длиной 3 символа в вопросительных предложениях ("Что", "тут", "Как")
            var result = text.SenchInQuestionSent(3);

            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Contains.Item("что"));
            Assert.That(result, Contains.Item("тут"));
            Assert.That(result, Contains.Item("как"));
        }

        // 3. Тест удаления слов заданной длины, начинающихся на согласную букву
        [Test]
        public void RemoveForLength_RemovesWordsStartingWithConsonant()
        {
            // "кот" (3 буквы, на 'к' - согласная) -> удаляется
            // "дом" (3 буквы, на 'д' - согласная) -> удаляется
            // "имя" (3 буквы, на 'и' - гласная) -> остается
            string sampleText = "Кот и имя дом.";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var reader = new StreamReader(stream);

            Text text = TextParcer.Parse(reader);
            text.RemoveForLength(3);

            string resultText = text.ToString();

            Assert.That(resultText, Does.Not.Contain("Кот"));
            Assert.That(resultText, Does.Not.Contain("дом"));
            Assert.That(resultText, Does.Contain("имя"));
        }

        
        // 4. Тест замены слов заданной длины в конкретном предложении
        [Test]
        public void Change_ReplacesWordsOfTargetLengthInSpecificSentence()
        {
            // В первом предложении слова "Мама" (4) и "мыла" (4) имеют длину 4, а "окно" (4) во втором
            // Добавим слово "пол" (3) для проверки, что меняются только слова нужной длины
            string sampleText = "Мама мыла пол. Папа мыла окно.";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var reader = new StreamReader(stream);

            Text text = TextParcer.Parse(reader);

            // Меняем в первом предложении (индекс 0) слова длиной 4 символа на "REPLACED"
            text.Change("REPLACED", 0, 4);

            Assert.That(text.Sentences[0].ToString(), Is.EqualTo("REPLACED REPLACED пол."));
            // Второе предложение не должно измениться
            Assert.That(text.Sentences[1].ToString(), Is.EqualTo("Папа мыла окно."));
        }

        // 5. Тест сортировки предложений по количеству слов (по убыванию)
        [Test]
        public void OutForWordCount_SortsSentencesByWordCountDescending()
        {
            string sampleText = "Одно слово. Тут три слова здесь. Два слова.";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var reader = new StreamReader(stream);

            Text text = TextParcer.Parse(reader);
            text.OutForWordCount();

            Assert.That(text.Sentences[0].WordCount, Is.EqualTo(4)); // "Тут три слова здесь."
            Assert.That(text.Sentences[1].WordCount, Is.EqualTo(2)); // "Два слова."
            Assert.That(text.Sentences[2].WordCount, Is.EqualTo(2)); // "Одно слово."
        }

        // 6. Тест удаления стоп-слов из потока
        [Test]
        public void RemoveStopWord_RemovesMatchingWordsFromText()
        {
            string sampleText = "Привет и добро пожаловать в мир.";
            string stopWordsList = "и\r\nв";

            using var textStream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText));
            using var textReader = new StreamReader(textStream);
            Text text = TextParcer.Parse(textReader);

            using var stopStream = new MemoryStream(Encoding.UTF8.GetBytes(stopWordsList));
            using var stopReader = new StreamReader(stopStream);

            text.RemoveStopWord(stopReader);

            string result = text.ToString();
            Assert.That(result, Does.Not.Contain(" и "));
            Assert.That(result, Does.Not.Contain(" в "));
            Assert.That(result, Is.EqualTo("Привет добро пожаловать мир."));
        }
    }
}