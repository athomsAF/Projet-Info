using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static projects.Pixel;
using System.Collections;

namespace projects
{
    internal class Huffman
    {
        public Noeud root;
        public Dictionary<Color, int> arbre;

        public Huffman(Pixel[,] image)
        {
            arbre = new Dictionary<Color, int>();

            for(int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Color c = new Color(image[i, j].R, image[i, j].G, image[i, j].B);

                    Color[] a = arbre.Keys.Where(o => (o.R == c.R && o.G == c.G && o.B == c.B)).ToArray();

                    if (a.Length == 0)
                    {
                        arbre.Add(new Color(c.R, c.G, c.G), 1);
                    }
                    else
                    {
                        arbre[a[0]] += 1;
                    }
                }
            }

            List<Noeud> nodes = new List<Noeud>();

            Console.WriteLine("Couleur & Fréquences :");

            foreach (Color key in arbre.Keys)
            {
                Console.WriteLine("(" + key.R + "," + key.G + "," + key.B + ") = " + arbre[key]);
            }

            foreach (Color key in arbre.Keys)
            {
                nodes.Add(new Noeud(key, arbre[key], null, null));
            }

            while (nodes.Count != 1)
            {
                Noeud n1 = MinFreqFromNodeList(nodes);
                nodes.Remove(n1);
                Noeud n2 = MinFreqFromNodeList(nodes);
                nodes.Remove(n2);

                Noeud join = new Noeud(new Color(-1, -1, -1), n1.frequence + n2.frequence, n1, n2);
                nodes.Add(join);
            }

            this.root = nodes[0];
        }

        /// <summary>
        /// DFS revisitée :-)
        /// </summary>
        /// <returns></returns>
        public List<int> TreeEncode()
        {
            List<int> encoding = new List<int>();

            Stack<Noeud> pile = new Stack<Noeud>();

            pile.Push(this.root);

            while(pile.Count != 0)
            {
                if(pile.Last().noeudGauche == null && pile.Last().noeudDroit == null)
                {
                    encoding.Add(1);
                    pile.Pop();
                    // !!! Ajoutter encodage pixel
                    encoding.Add(2);
                }
                else
                {
                    encoding.Add(0);
                    pile.Pop();

                    if (pile.Last().noeudDroit != null)
                    {
                        pile.Push(pile.Last().noeudDroit);
                    }

                    if (pile.Last().noeudGauche != null)
                    {
                        pile.Push(pile.Last().noeudGauche);
                    }
                }
            }

            return encoding;
        }

        public Noeud TreeDecode(BitArray liste)
        {
            Noeud root = null;

            if (liste.Count == 0)
            {
                return null;
            }
            if (liste[0] == true)
            {
                Color pix = new Color(GetByteFromBitArray(liste, 1), GetByteFromBitArray(liste, 9), GetByteFromBitArray(liste, 17));
                root = new Noeud(pix, 0, null, null);
            }
            else
            {
                Stack<Noeud> pile = new Stack<Noeud>();
                root = new Noeud(null, 0, null, null);
                pile.Push(root);

                for (int i = 1; i < liste.Length; i++)
                {
                    if (liste[i] == false)
                    {
                        pile.Push(new Noeud(null, 0, null, null));
                    }
                    else
                    {
                        Color pix = new Color(GetByteFromBitArray(liste, i + 1), GetByteFromBitArray(liste, i + 8), GetByteFromBitArray(liste, i + 16));

                        if (pile.Last().noeudGauche != null)
                        {
                            pile.Last().noeudDroit = new Noeud(pix, 0, null, null);

                            while (pile.Last().noeudGauche != null && pile.Last().noeudDroit != null)
                            {
                                Noeud temp = pile.Last();
                                pile.Pop();
                                if (pile.Last().noeudGauche != null)
                                {
                                    pile.Last().noeudDroit = temp;
                                }
                                else
                                {
                                    pile.Last().noeudGauche = temp;
                                }
                            }
                        }
                        else
                        {
                            pile.Last().noeudGauche = new Noeud(pix, 0, null, null);                           
                        }

                        i += 3 * 8;
                    }
                }
            }
            return root;
        }

        static byte GetByteFromBitArray(BitArray bitArray, int start)
        {
            byte b = 0;

            if (bitArray.Count - start >= 8)
            {
                BitArray bits = new BitArray(8);

                for (int i = start; i < start + 8; i++)
                {
                    bits[i] = bitArray[start + i];
                }

                byte[] bytes = new byte[1];

                bits.CopyTo(bytes, 0);

                b = bytes[0];
            }

            return b;
        }

        public void AfficherBinaireArbre()
        {
            Console.WriteLine("Couleur & Binaire");
            if (this.root != null && this.arbre != null)
            {
                foreach (Color key in this.arbre.Keys)
                {
                    List<bool> test = this.root.BinaireEnfants(key, new List<bool>());

                    if (test != null)
                    {
                        Console.Write("(" + key.R + "," + key.G + "," + key.B + ") = ");

                        foreach (bool bin in test)
                        {
                            if (bin)
                            {
                                Console.Write("1");
                            }
                            else
                            {
                                Console.Write("0");
                            }

                        }

                        Console.WriteLine();
                    }
                }
            }
        }

        public static Noeud MinFreqFromNodeList(List<Noeud> liste)
        {
            Noeud node = null;
            int min;

            if (liste != null && liste.Count >= 0)
            {
                node = liste[0];
                min = node.frequence;

                foreach (Noeud nodes in liste)
                {
                    if (nodes.frequence < min)
                    {
                        min = nodes.frequence;
                        node = nodes;
                    }
                }
            }

            return node;
        }
    }

    internal class Noeud
    {
        public Color ?pixel;
        public int frequence;
        public Noeud ?noeudDroit;
        public Noeud ?noeudGauche;

        public Noeud(Color pixel, int frequence, Noeud? noeudGauche, Noeud ?noeudDroit)
        {

            this.pixel = new Color(pixel.R, pixel.G, pixel.B);

            this.frequence = frequence;
            this.noeudDroit = noeudDroit;
            this.noeudGauche = noeudGauche;
        }

        public List<bool> BinaireEnfants(Color pixel, List<bool> ?liste = null)
        {
            // Return the list if the pixel researched correspond to this node
            if(this.pixel != null  && this.pixel.R == pixel.R && this.pixel.G == pixel.G && this.pixel.B == pixel.B)
            {
                return liste;
            }
            else
            {
                // Intial values
                List<bool> a = null;
                List<bool> b = null;

                // Verify if the searched pixel exists on the right child node (return != null)
                if (this.noeudDroit != null)
                {
                    a = noeudDroit.BinaireEnfants(pixel, liste);
                }

                // Verify if the serached pixel exists on the left child node (return != null)
                if (this.noeudGauche != null)
                {
                    b = noeudGauche.BinaireEnfants(pixel, liste);
                }

                // If the node exist on the left side, add the binary digit and return the list
                if(a == null && b!= null)
                {
                    b.Add(true);
                    return b;
                }

                // If the node exist on the right side, add the binary digit and return the list
                else if (a != null && b == null)
                {
                    a.Add(false);
                    return a;
                }

            }
            // If none of the baove, return null (not found)

            return null;

        }
    }
}
