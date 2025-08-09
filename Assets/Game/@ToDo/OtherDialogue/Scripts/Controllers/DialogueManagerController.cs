namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;

    public class DialogueManagerController : MonoBehaviour
    {
        private Queue<Sprite> sprites;
        private Queue<string> sentences;
        private Queue<AudioClip> voices;
        private AudioClip audioQueue;

        private string sentence;
        private Expression expression;

        private List<LetterComponent> letters;
        private List<float> speeds;
        private List<ITextEffectBuilder> effects;
        private int fontSize = 30;
        private int boxSize = 380;
        private int currentX = 0;
        private int currentY = 0;

        private float currentSpeed = 0.01f;
        private ITextEffectBuilder currentEffect = null;

        private DialogueData model;
        
        public DialogueManagerController(DialogueData newModel)
        {
            model = newModel;
            sentences = new Queue<string>();
            sprites = new Queue<Sprite>();
            voices = new Queue<AudioClip>();
            letters = new List<LetterComponent>();
            speeds = new List<float>();
            effects = new List<ITextEffectBuilder>();
        }


        public void StartDialogue()
        {
            Dialogue dialogue = model.DialogueToShow;
            model.DialogueToShow = null;
            model.Animator.SetBool("IsOpen", true);
            voices.Clear();
            sprites.Clear();
            sentences.Clear();

            foreach (Sentence sentence in dialogue.Sentences)
            {
                expression = sentence.Character.Expressions[sentence.ExpressionIndex];
                sprites.Enqueue(expression.Image);
                sentences.Enqueue(sentence.SentenceText);
                voices.Enqueue(sentence.Character.Voice);
            }
        }

        public bool DisplayNextSentence()
        {
            foreach (LetterComponent letter in letters)
            {
                Destroy(letter.gameObject);
            }

            currentSpeed = model.WaitTime;
            currentEffect = null;
            effects.Clear();
            speeds.Clear();
            letters.Clear();
            currentX = 0;
            currentY = 0;

            if (sentences.Count == 0)
            {
                EndDialogue();
                return false;
            }

            model.ImageText.sprite = sprites.Dequeue();
            sentence = sentences.Dequeue();
            audioQueue = voices.Dequeue();
            model.WaitTime = 0f;
            string onlyWords = string.Empty;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == '[')
                {
                    i = changeSpeed(i);
                }
                else if (sentence[i] == '<')
                {
                    i = changeEffect(i);
                }
                else
                {
                    effects.Add(currentEffect);
                    if (sentence[i] != ' ')
                    {
                        speeds.Add((float)currentSpeed);
                    }
                    onlyWords += sentence[i];
                }
            }

            string[] words = onlyWords.Split(' ');
            int letterSpacing = (int)(fontSize * 0.5);
            int currentIndexEffects = 0;
            int currentIndexSpeeds = 0;

            foreach (string word in words)
            {
                GameObject wordObject = new GameObject(word, typeof(RectTransform));
                wordObject.transform.SetParent(model.DialogueStartPoint);
                int wordSize = word.Length * letterSpacing;
                if (currentX + wordSize > boxSize)
                {
                    currentX = 0;
                    currentY -= (int)(fontSize * 0.9);
                }
                wordObject.GetComponent<RectTransform>().localPosition = new Vector3(currentX, currentY, 0);

                for (int i = 0; i < word.Length; i++)
                {
                    GameObject letterObject = new GameObject(word[i].ToString());
                    letterObject.transform.SetParent(wordObject.transform);
                    Text myText = letterObject.AddComponent<Text>();
                    myText.text = word[i].ToString();
                    myText.alignment = TextAnchor.LowerCenter;
                    myText.fontSize = fontSize;
                    myText.font = model.Font;
                    myText.material = model.Material;
                    myText.GetComponent<RectTransform>().localPosition = new Vector3(i * letterSpacing, 0, 0);
                    myText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    RectTransform rt = letterObject.GetComponentInParent<RectTransform>();
                    rt.sizeDelta = new Vector2(fontSize, fontSize);
                    rt.pivot = new Vector2(0, 1);

                    LetterComponent letterComponent = letterObject.AddComponent<LetterComponent>();
                    
                    Letter newLetter = new Letter
                    {
                        Character = word[i],
                        Speed = speeds[currentIndexSpeeds],
                        isActive = false
                    };
                    if (effects[currentIndexEffects] != null)
                    {
                        newLetter.Effect = effects[currentIndexEffects].Build(letterObject);
                    }
                    letterComponent.Model = newLetter;
                    letters.Add(letterComponent);
                    currentIndexEffects++;
                    currentIndexSpeeds++;
                }
                currentX += wordSize + letterSpacing;
                currentIndexEffects++;
            }
            return true;
        }

        public int changeSpeed(int i)
        {
            i++;
            string speed = string.Empty;
            while (sentence[i] != ']')
            {
                speed += sentence[i];
                i++;
            }
            currentSpeed = float.Parse(speed);
            return i;
        }

        public int changeEffect(int i)
        {
            i++;
            string effect = string.Empty;
            while (sentence[i] != '>')
            {
                effect += sentence[i];
                i++;
            }

            if (TextEffect.effects.ContainsKey(effect))
            {
                currentEffect = TextEffect.effects[effect];
            }
            else
            {
                currentEffect = null;
            }
            return i;
        }

        public IEnumerator TypeSentence()
        {
            foreach (LetterComponent letter in letters)
            {
                if (letter == null)
                {
                    break;
                }

                model.Source.PlayOneShot(audioQueue, model.VoiceVolume);
                yield return new WaitForSeconds(letter.Model.Speed);
            }
            model.Finished = true;
        }

        public void EndDialogue()
        {
            model.Animator.SetBool("IsOpen", false);
        }

        /// <summary>
        /// Parses the sentence, for fully displaying it.
        /// </summary>
        /// <param name="sentence">Sentence to be parsed.</param>
        /// <returns>Returns the complete sentence witout the [time] labels</returns>
        private string ParseSentence(string sentence)
        {
            string parsedSentence = "";
            bool normalSentence = true;
            foreach (char letter in sentence.ToCharArray())
            {
                if (letter == '[')
                {
                    normalSentence = false;
                }

                if (letter == ']')
                {
                    normalSentence = true;
                }

                if (normalSentence)
                {
                    if (letter != ']')
                    {
                        parsedSentence += letter;
                    }
                }
            }

            return parsedSentence;
        }
    }
}
