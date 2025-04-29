using UnityEngine;
using System.Xml;
using System.Collections.Generic;

/*
	Estructura del XML:
	<?xml (...)?>
	<root>

		<[question_id]>
			<answer>0</answer>
			<[language_1]>
				<title></title>
				<option></option>
				<option></option>
				<option></option>
			</[language_1]>
			<[language_2]>
				<title></title>
				<option>Option A</option>
				<option>Option B</option>
				<option>Option C</option>
			</[language_2]>
		</[question_id]>

	</root>
 */

namespace Isostopy.Quiz
{
	/// <summary>
	/// Componente que lee preguntas desde un xml y las añade al <see cref="Quiz"/> </summary>

	public class QuizFileReader : MonoBehaviour
	{
		[Space]
		[SerializeField] TextAsset[] files = { };


		// -----------------------------------

		public const string AnswerNodeName = "answer";
		public const string TitleNodeName = "title";
		public const string OptionNodeName = "option";

		// -----------------------------------------------------------------------

		private void Start()
		{
			foreach (var file in files)
			{
				ReadFile(file);
			}
		}

		private void ReadFile(TextAsset file)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(file.text);

			foreach (XmlNode questionNode in xmlDoc.DocumentElement)
			{
				QuizQuestion question = ReadQuestion(questionNode);
				Quiz.AddQuestion(question);
			}
		}

		private QuizQuestion ReadQuestion(XmlNode questionNode)
		{
			string id = questionNode.Name;
			string title = "";
			string[] options = null;
			int rightAnswer = -1;

			foreach (XmlNode node in questionNode.ChildNodes)
			{
				// Respuesta correcta
				if (node.Name == AnswerNodeName)
				{
					rightAnswer = int.Parse(node.InnerText);
					continue;
				}

				string language = node.Name;	// Idioma para el sistema de traducciones

				// Titulo
				XmlNode titleNode = node.SelectSingleNode(TitleNodeName);
				string titleTranslation = titleNode.InnerText;

#if ISOSTOPY_TRANSLATION
				string titleTranslationId = id + "_" + TitleNodeName;
				/*
					Si existe el sistema de traducciones añadimos el titulo con id
					y en QuizQuestion guardamos solo el id. 
				*/
#endif

				if (string.IsNullOrEmpty(title))
					title = titleTranslation;

				// Opciones
				XmlNodeList optionNodes = node.SelectNodes(OptionNodeName);
				List<string> optionTranslationList = new();
				for (int i = 0; i < optionTranslationList.Count; i++)
				{

#if ISOSTOPY_TRANSLATION
				string optionTranslationId = id + "_" + OptionNodeName + "_" + i;
				/*
					Si existe el sistema de traducciones lo que se guarda en el QuizQuestion es el id de la traduccion
					Añadimos aqui la traduccion de cada opcion al sistema de traducciones.
				*/
#endif

					XmlNode optionNode = optionNodes[i];
					optionTranslationList.Add(optionNode.InnerText);
				}

				if (options != null)
					options = optionTranslationList.ToArray();
			}

			QuizQuestion question = new(id, title, options, rightAnswer);
			return question;
		}
	}

}
