using System;
using System.Collections.Generic;
using System.Linq;

namespace FlattenList
{
    internal class Program
    {
		// ülessanne: implementeeri meetod "PrintList" nii et oodatud tulemus pinditakse konsooli

		// sisendandmed: sisendiks on objekt TestData, mida võib vaadelda Listina, mille igaks elemendiks on List või Väärtus
		// JSON'ina visualiseerituna oleks TestData: 
		/* {
			"a",
			{
				"b",
				"c",
				{
					"d",
					"e"
				},
				{
					"f"
				},
				"g"
			},
			"h",
			"i",
			{
				"j",
				"k"
			}
		} */
		// C# sisendandmete struktuur: 
		// * abstraktne klass Node -  sellest koosnevad List'id
		// * klass ValueNode:Node - klass millega hoitakse listis Väärtust
		// * klass ListNode:Node - klass millega hoitakse listis alamlisti
		 
		// oodatud tulemus:
		/*
		0:A
		1.0:B
		1.1:C
		1.2.0:D
		1.2.1:E
		1.3.0:F
		1.4:G
		2:H
		3:I
		4.0:J
		4.1:K
		*/
		// ehk prinditakse "elemendi positioon listis : väärtus" juhul kui tegu on alamlistiga, prinditakse selle ette "alamlisti positsioon listis"+'.' jne.

		// lahendamise juhised:
		// kasutada rekursiooni, tulemus peab olema korrektne iga sisendi korral
		// eeldada võib et Value/Values ei ole kunagi NULL (ValueNode.Values võib küll olla tühi list)
		// lahendus (funktsiooni PrintList sisu) peaks olema mõistliku pikkusega (mitte rohkem kui mõnikümmend rida)
		// soovikorral võib funktsiooni PrintList esimest väljakutsed (Main() funktsioonist) muuta

		private static void Main()
        {
			//prindib tulemuse
			PrintList(TestData);
			//ootab kasutaja inputi enne akna sulgemist
            Console.ReadLine();
        }

        private static void PrintList(List<Node> testData, IList<int> levels = null)
        {
            if (levels == null) levels = new List<int>();

            for (int i = 0; i < testData.Count; i++)
            {
                if (testData[i] is ListNode)
                {
                    levels.Add(i);
                    PrintList((testData[i] as ListNode).Values, levels);
                    levels.RemoveAt(levels.Count - 1);
                    continue;
                }

                if (!(testData[i] is ValueNode)) continue;

                if (levels.Count > 0) Console.Write(string.Join(".", levels) + ".");
                Console.WriteLine(i + ":" + (testData[i] as ValueNode).Value.ToUpper());
            }
        }

        public static readonly List<Node> TestData = new List<Node>
        {
            new ValueNode {Value = "a"},
            new ListNode
            {
                Values = new List<Node>
                {
                    new ValueNode {Value = "b"},
                    new ValueNode {Value = "c"},
                    new ListNode
                    {
                        Values = new List<Node>
                        {
	                        new ValueNode {Value = "d"},
	                        new ValueNode {Value = "e"}
                        }
                    },
                    new ListNode
                    {
                        Values = new List<Node>
                        {
                            new ValueNode {Value = "f"}
                        }
                    },
                    new ValueNode {Value = "g"}
                }
            },
            new ValueNode {Value = "h"},
            new ValueNode {Value = "i"},
            new ListNode
            {
                Values = new List<Node>
                {
                    new ValueNode {Value = "j"},
                    new ValueNode {Value = "k"}
                }
            },
        };

		/// <summary>
		/// abstraktne klass, kõik List'id koosnevad selle klassi alamklassidest
		/// </summary>
        public abstract class Node
        {
            protected object Data;
        }
		/// <summary>
		/// klass väärtuse hoidmiseks
		/// </summary>
        public class ValueNode : Node
        {
            public string Value
            {
                get { return (string)Data; }
                set { Data = value; }
            }
        }
		/// <summary>
		/// klass alamlisti hoidmiseks
		/// </summary>
        public class ListNode : Node
        {
            public List<Node> Values
            {
                get { return (List<Node>)Data; }
                set { Data = value; }
            }
        }
    }
}
