
namespace Isostopy.Quiz
{
	/// <summary>
	/// Pregunta de tipo test con varias opciones y la respuesta correcta. </summary>

	public class QuizQuestion
	{
		public string id;
		public string title;
		public string[] options;
		public int answer;
		/* Podria molar hacer right answer un array y permitir varias opciones correctas. */

		public QuizQuestion(string id, string title, string[] options, int answer)
		{
			this.id = id;
			this.title = title;
			this.options = options;
			this.answer = answer;
		}

		public QuizQuestion(string id, string[] options, int answer)
		{
			this.id = id;
			this.title = "";
			this.options = options;
			this.answer = answer;
		}
	}

}
