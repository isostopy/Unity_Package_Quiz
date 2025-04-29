using UnityEngine;
using System.Collections.Generic;

namespace Isostopy.Quiz
{
	/// <summary>
	/// Clase estatica que contiene toda la bateria de preguntas. </summary>

	public static class Quiz
	{
		private static Dictionary<string, QuizQuestion> questions = new();


		// ----------------------------------------------------------------------------------

		/// <summary> Añade una pregunta a la bateria de preguntas. </summary>
		public static void AddQuestion(QuizQuestion question)
		{
			if (questions.ContainsKey(question.id))
			{
				Debug.LogWarning("La bateria de preguntas ya contiene una pregunta con el id [" + question.id + "]");
				return;
			}
			questions.Add(question.id, question);
		}

		/// <summary> Obten la pregunta con el id indicado. </summary>
		public static QuizQuestion GetQuestion(string id)
		{
			if (questions.ContainsKey(id) == false)
			{
				Debug.LogWarning("La bateria de preguntas no contiene una pregunta con el id [" + id + "]");
				return null;
			}
			return questions[id];
		}
	}

}
