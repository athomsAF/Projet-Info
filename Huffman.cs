using static projects.Pixel;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace projects
{
    /// <summary>
    /// Huffman class
    /// </summary>
    internal class Huffman
    {
        // Attributes
        public Node root;
        public Dictionary<Pixel, int> ?pixelFrequencyTable;

        /// <summary>
        /// bitssic constructor (root node only)
        /// </summary>
        /// <param name="root">Root node containing the node's branching</param>
        public Huffman(Node root)
        {
            if(root != null)
            {
                this.root = root;
            }
            else
            {
                this.root = new Node();
            }

            this.pixelFrequencyTable = null;
        }

        /// <summary>
        /// Build a Huffman tree and a corresultpondance table for each pixel an their frequency
        /// </summary>
        /// <param name="image"> </param>
        public Huffman(Pixel[,] image)
        {
            if(image != null && image.Length > 0)
            {
                // Create a new table
                this.pixelFrequencyTable = new Dictionary<Pixel, int>();

                // Loop throught each pixel of the image
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        // Get the Pixel
                        Pixel c = image[i, j];

                        // Find and increase the pixel frequency if it exists in the table, or add a new key the it
                        Pixel[] a = pixelFrequencyTable.Keys.Where(o => o.RI == c.RI && o.GI == c.GI && o.BI == c.BI).ToArray();

                        if (a.Length == 0)
                        {
                            pixelFrequencyTable.Add(c, 1);
                        }
                        else
                        {
                            pixelFrequencyTable[a[0]] += 1;
                        }
                    }
                }

                // Create every new nodes from the corresultpondance table
                List<Node> nodes = new();

                foreach (Pixel key in pixelFrequencyTable.Keys)
                {
                    nodes.Add(new Node(key, pixelFrequencyTable[key], null, null));
                }

                // Bind every nodes with the lowest frequency, add the new node to the list and keep doign it until there is one node left (root)
                while (nodes.Count != 1)
                {
                    Node ?n1 = MinFreqFromNodeList(nodes);
                    if(n1 != null) nodes.Remove(n1);

                    Node ?n2 = MinFreqFromNodeList(nodes);
                    if(n2 != null) nodes.Remove(n2);

                    if (n1 != null && n2 != null)
                    {
                        Node join = new(null, n1.frequency + n2.frequency, n1, n2);
                        nodes.Add(join);
                    }
                }

                this.root = nodes[0];
            }
            else
            {
                this.root = new Node();
                this.pixelFrequencyTable = null;
            }
        }

        /// <summary>
        /// Reccursive algorithm to encode the Huffman tree in a array of bits (0 : branching / 1 : leaf) 
        /// </summary>
        /// <returns>Array of bits : tree encoding</returns>
        public List<bool> TreeEncode(Node node)
        {
            List<bool> result = new();

            // If branching node
            if (node.pixel == null && node.leftNode != null && node.rigthNode != null)
            {
                // Get encoded value of the tree of each sub-nodes
                List<bool> subTree1 = TreeEncode(node.leftNode);
                List<bool> subTree2 = TreeEncode(node.rigthNode);

                // Add branching code and the sub-tree values
                result.Add(false);
                result.AddRange(subTree1);
                result.AddRange(subTree2);

                return result;
            }
            // If leaf node
            else if (node.pixel != null)
            {
                // Add leaf code and bit encoded pixel array 
                result.Add(true);

                List<bool> pixelBitArray = GetBitArrayFromPixel(node.pixel).ToList();

                result.AddRange(pixelBitArray);
            }

            return result;
        }

        /// <summary>
        /// Decode of the Huffman tree from an array of bits
        /// </summary>
        /// <param name="bitList">list of all the bites</param>
        /// <returns></returns>
        public static Node TreeDecode(List<bool> bitList)
        {
            Node root = new();

            // If the root node is a leaf
            if (bitList[0] == true)
            {
                Pixel pix = GetPixelFromBitArray(bitList, 1);
                root = new Node(pix, 0, null, null);
            }

            // If the first node is a branch
            else
            {
                // Remove the node code at the begining of the bit array, create a new root node and add it to the stack
                bitList.RemoveAt(0);
                Stack<Node> pile = new();
                pile.Push(root); // Add root node

                // Loop while the stack and the bit array aren't empty 
                do
                {
                    // Branching node, extract node information and remove it from the bit array
                    if (bitList[0] == false)
                    {
                        bitList.RemoveAt(0);
                        pile.Push(new Node());
                    }

                    // Leaf node, extract node information and remove it from the bit array
                    else
                    {
                        bitList.RemoveAt(0);
                        Pixel Feuille = GetPixelFromBitArray(bitList, 0);
                        Node FeuilleNode = new(Feuille, 0, null, null);
                        bitList.RemoveRange(0, 24);

                        // Fill the parent node accordignly to the pre-existing chil node completion
                        if (pile.Last().leftNode == null)
                        {
                            pile.Last().leftNode = FeuilleNode;
                        }
                        else if (pile.Last().rigthNode == null)
                        {
                            pile.Last().rigthNode = FeuilleNode;
                        }

                        // Remove the last branching node from the stack and add it to their parent node, repeat until the partent node isn't full 
                        while (pile.Count != 0 && pile.Last().leftNode != null && pile.Last().rigthNode != null)
                        {
                            Node temp = pile.Last();
                            pile.Pop();

                            if (pile.Count == 0)
                            {
                                root = temp;
                            }
                            else if (pile.Last().leftNode == null)
                            {
                                pile.Last().leftNode = temp;
                            }
                            else if (pile.Last().rigthNode == null)
                            {
                                pile.Last().rigthNode = temp;
                            }
                        }
                    }
                } while (bitList.Count > 0 && pile.Count != 0);
            }
            return root;
        }

        /// <summary>
        /// Convert a 24 long bit arry from a starting index from a bit array to a pixel
        /// </summary>
        /// <param name="bitArray">Bit array containing the pixel to be extracted</param>
        /// <param name="start">Stating index</param>
        /// <returns>Extracted pixel (default : black pixel)</returns>
        public static Pixel GetPixelFromBitArray(List<bool> bitArray, int start)
        {
            // Default returned Pixel
            Pixel col = new(0,0,0);

            if (start >= 0 && bitArray != null && bitArray.Count - start >= 24)
            {
                List<byte> list = new();

                // For each set of 8 pixel, convert to byte and add to the list
                for (int j = 0; j < 3; j++)
                {
                    byte[] newByte = new byte[1];
                    BitArray bits = new(8);
                    int k = 0;

                    for (int i = start + j * 8; i < start + (j + 1) * 8; i++)
                    {
                        bits[k] = bitArray[start + i];
                        k++;
                    }

                    bits.CopyTo(newByte, 0);

                    list.Add(newByte[0]);
                }

                // Dispatch the extracted Pixel components to a new Pixel
                col = new Pixel(list[0], list[1], list[2]);
            }

            return col;
        }

        /// <summary>
        /// Convert a pixel to his equivalent as a bits array
        /// </summary>
        /// <param name="pixel">Pixel to convert</param>
        /// <returns></returns>
        public static List<bool> GetBitArrayFromPixel(Pixel pixel)
        {
            List<bool> result = new();

            if (pixel != null)
            {
                byte[] bytes = new byte[] { pixel.R, pixel.G, pixel.B };

                // Convert each byte to a bit array and add it to the final list
                BitArray bitArray = new(bytes);

                for (int i = 0; i < 24; i++)
                {
                    result[i] = bitArray[i];
                }
            }

            return result;
        }

        /// <summary>
        /// Write down each pixel and their corresponding huffamn encoded value
        /// </summary>
        public void ShowPixelEncoding()
        {
            if (this.root != null && this.pixelFrequencyTable != null)
            {
                foreach (Pixel key in this.pixelFrequencyTable.Keys)
                {
                    List<bool> ?test = this.root.HuffmanCode(key, new List<bool>());

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

        /// <summary>
        /// Get the node with the minimum frequency in a list
        /// </summary>
        /// <param name="nodeList">List of nodes</param>
        /// <returns>Node with minimal frequency</returns>
        public static Node ?MinFreqFromNodeList(List<Node> nodeList)
        {
            Node ?node = null;
            int min;

            if (nodeList != null && nodeList.Count >= 0)
            {
                node = nodeList[0];
                min = node.frequency;

                // Look for the minimum frequency in the node list and update the corresponding node
                foreach (Node nodes in nodeList)
                {
                    if (nodes.frequency < min)
                    {
                        min = nodes.frequency;
                        node = nodes;
                    }
                }
            }

            return node;
        }

        /// <summary>
        /// Encode a pixel image to a bit array
        /// </summary>
        /// <param name="image">Image to be encoded</param>
        /// <param name="showMatrixBeforeEncoding">Option to show the resulting encoded matrix before converting it to an array</param>
        /// <returns>Bit array of all the pixel encoded values of the immage</returns>
        public List<bool> ImageEncode(Pixel[,] image, bool showMatrixBeforeEncoding = false)
        {
            List<bool> bits = new();

            if(image != null && image.Length >= 0)
            {
                // Convert the pixel matrix to an Huffman encoded pixel matrix
                List<bool>[,] encodedImage = new List<bool>[image.GetLength(0),image.GetLength(1)];

                for (int row = 0; row < image.GetLength(0); row++)
                {
                    for (int col = 0; col < image.GetLength(1); col++)
                    {
                        // Encode pixel
                        List<bool> ?a = this.root.HuffmanCode(image[row, col], new List<bool>());
                        if(a != null) a.Reverse();
                        List<bool>? encodedPixel = a ;

                        if (encodedPixel != null)
                        {
                            encodedImage[row, col] = new List<bool>();
                            encodedImage[row, col].AddRange(encodedPixel);

                            bits.AddRange(encodedPixel);
                        }
                    }
                }

                if (showMatrixBeforeEncoding) ShowHuffmanEncodedImage(encodedImage);

            }
            return bits;
        }

        /// <summary>
        /// Decode an bit array to a pixel image using an huffman tree
        /// </summary>
        /// <param name="image">Bit array to be decoded</param>
        /// <param name="heigth">Height of the final image (known from the header)</param>
        /// <param name="width">Width of the final image -known from the header)</param>
        /// <returns>Pixel image</returns>
        public Pixel[,] ImageDecode(List<bool> image, int heigth, int width)
        {
            Pixel[,] result = new Pixel[heigth, width];

            if (image != null && image.Count >= 0)
            {
                // Decode a pixel matrix using a huffman tree and an pixel encoded array
                for (int row = 0; row < heigth; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        result[row, col] = Node.HuffmanDecode(this.root, image);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Show each encoded pixel value in an encoded image
        /// </summary>
        /// <param name="encodedImage">Huffamn encoded image</param>
        public static void ShowHuffmanEncodedImage(List<bool>[,] encodedImage)
        {
            if(encodedImage != null && encodedImage.Length >= 0)
            {
                // Loop through each encoded pixel in the image
                for(int row = 0; row < encodedImage.GetLength(0); row ++)
                {
                    for(int col = 0; col < encodedImage.GetLength(1); col++)
                    {
                        // Write binary encoded value
                        foreach(bool bit in encodedImage[row, col])
                        {
                            if (bit) Console.Write("1"); else Console.Write("0");
                        }

                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }
            }
        }
    }

    /// <summary>
    /// Node / Tree
    /// </summary>
    internal class Node
    {
        // Attributes
        public Pixel ?pixel;
        public int frequency;
        public Node ?rigthNode;
        public Node ?leftNode;

        /// <summary>
        /// Constructor for an empty node
        /// </summary>
        public Node()
        {
            this.pixel = null;
            this.frequency = 0;
            this.rigthNode = null;
            this.leftNode = null;
        }

        /// <summary>
        /// bitssic constructor of the Node class
        /// </summary>
        /// <param name="pixel">Pixel</param>
        /// <param name="frequency">Frequency the pixel</param>
        /// <param name="leftNode">Left child node</param>
        /// <param name="rigthNode">Right child node</param>
        public Node(Pixel ?pixel, int frequency, Node? leftNode, Node? rigthNode)
        {
            if(pixel == null)
            {
                this.pixel = null;
            }
            else
            {
                this.pixel = new Pixel(pixel.R, pixel.G, pixel.B);
            }

            this.frequency = frequency;
            this.rigthNode = rigthNode;
            this.leftNode = leftNode;
        }

        ///<summary>
        /// Itterative algortihm to show the node tree horizontally
        /// </summary>
        /// <param name="node">Parent node</param>
        /// <param name="depth">Depth of the node (add spaces before writing down the children nodes)</param>
        public static void ShowTree(Node node, int depth = 0)
        {
            string spaces = Program.Spaces('|', depth);

            if (node == null)
            {
                Console.WriteLine("Null node");
            }
            else if (node.pixel == null && node.rigthNode != null && node.leftNode != null)
            {
                Console.WriteLine(spaces + "Start");

                ShowTree(node.rigthNode, depth + 1);

                ShowTree(node.leftNode, depth + 1);

                Console.WriteLine(spaces + "End");
            }
            else if (node.pixel != null)
            {
                Console.WriteLine(spaces + node.pixel.ToString());
            }
        }

        /// <summary>
        /// Get the Huffman code of a child node in the tree
        /// !!! Give the REVERSED code
        /// </summary>
        /// <param name="pixel">Searched pixel</param>
        /// <param name="HuffmanCode"></param>
        /// <returns>Huffman code of the pixel</returns>
        public List<bool> ?HuffmanCode(Pixel pixel, List<bool> ?HuffmanCode = null)
        {
            // Return the Huffman code if the pixel searched corresultpond to this node
            if(this.pixel != null  && this.pixel.R == pixel.R && this.pixel.G == pixel.G && this.pixel.B == pixel.B)
            {
                return HuffmanCode;
            }
            else
            {
                // Intial child List
                List<bool> ?a = null;
                List<bool> ?b = null;

                // Verify if the searched pixel exists in the right child tree (return != null)
                if (this.rigthNode != null)
                {
                    a = this.rigthNode.HuffmanCode(pixel, HuffmanCode);
                }

                // Verify if the serached pixel exists on the left child tree (return != null)
                if (this.leftNode != null)
                {
                    b = this.leftNode.HuffmanCode(pixel, HuffmanCode);
                }

                // If the node exist on the left side, add the binary digit and return the list
                if(a == null && b != null)
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
            // If none of the above, return null (not found)
            return null;
        }

        /// <summary>
        /// Decode a pixel from a bit array using an huffman tree
        /// </summary>
        /// <param name="root">Roor node of the Huffman tree</param>
        /// <param name="bitArray">Bit array to be decoded</param>
        /// <returns>Pixel extracted from the bit array</returns>
        public static Pixel HuffmanDecode(Node root, List<bool> bitArray)
        {
            // Leaf node : return it's value
            if(root.pixel != null)
            {
                return new Pixel(root.pixel.R, root.pixel.G, root.pixel.B);
            }
            // Branching node : search for the correponding child node
            else if (root.leftNode != null && root.rigthNode != null && bitArray.Count > 0)
            {
                // 1 : left node
                if (bitArray[0])
                {
                    bitArray.RemoveAt(0);
                    return HuffmanDecode(root.leftNode, bitArray);
                }
                // 0 : right node
                else
                {
                    bitArray.RemoveAt(0);
                    return HuffmanDecode(root.rigthNode, bitArray);
                }
            }

            return new Pixel();
        }

        /// <summary>
        /// Description of the node
        /// </summary>
        /// <returns>Description : Pixel / Frequency / Child nodes</returns>
        public override string ToString()
        {
            string str = "";

            if(this.pixel != null)
            {
                str += this.pixel.ToString() + "\n";
            }
            else
            {
                str += "Parent Node\n";
            }

            str += "Freq : " + this.frequency + "\n";

            if(this.rigthNode != null && this.rigthNode.pixel != null && this.leftNode != null && this.leftNode.pixel != null)
            {
                str += "Node 1 : " + this.leftNode.pixel.ToString();

                str += "Node 2 : " + this.rigthNode.pixel.ToString();
            }

            return str;
        }
    }
}
