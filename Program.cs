﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
namespace DataStructures_Assignment {
  class Program {
    static void Main(string[] args) {
      //C:\Users\124be\Desktop\numbers.txt
      Console.WriteLine("Array Task");
      arrayTask();
      Console.ReadKey();
      Console.Clear();
      Console.WriteLine("Doubly Linked List Task");
      doublyLinkedListTask();
      Console.ReadKey();
      Console.Clear();
      Console.WriteLine("Binary Search Tree Task");
      binarySearchTreeTask();
      Console.ReadKey();
      Console.Clear();
      Console.WriteLine("Hashtable Task");
      hashtableTask();
      Console.ReadKey();
    }
    private static void arrayTask() {
      string path;
      do {
        Console.Write("\nPath for Array Task: ");
        path = Console.ReadLine();
        Console.Clear();
      } while (path.Length == 0 || !File.Exists(path));
      string[] data;
      try {
        data = File.ReadAllLines(path); //Get file data
      } catch {
        Console.WriteLine($"Error reading file: {path}");
        return;
      }
      int[] numbers = new int[data.Length];
      int current;
      int countPrime = 0;
      for (int i = 0; i < data.Length; i++) { //Loop through all lines in file
        if (!int.TryParse(data[i], out current)) //Convert to int
          throw new Exception("File contains invalid number. Last line in file is possibly a new line"); //Failed to convert: throw exception
        numbers[i] = current; //Set array
        countPrime += isPrime(current) ? 1 : 0; //Count primes
        Console.WriteLine(current); //Log number
      }
      Console.WriteLine($"Amount of Primes: {countPrime}");
    }
    private static void doublyLinkedListTask () {
      int numOfFiles = 0;
      //Get number of files
      do {
        Console.Write("Number of files: ");
        if (!int.TryParse(Console.ReadLine(), out numOfFiles) && numOfFiles >= 0) { //If input is not a int or positive
          Console.Clear();
          Console.Write("Invalid input. Try again\nNumber of files: "); //Ask again
        } else break;
      } while (true);
      
      Console.Clear();
      if (numOfFiles == 0) //Return if there are no files to enter
        return;
      string path;
      string[] data;
      LinkedList<int> list;
      LinkedListNode<int> follower;  
      LinkedListNode<int> leader;
      int primeCount;
      //Create linked lists from user entered files. Find middle, print primes of said file
      for (int i = 0; i < numOfFiles; i++) {
        do {
          Console.Write($"File [{i+1}], path: ");
          path = Console.ReadLine();
          Console.Clear();
        } while (path.Length == 0 || !File.Exists(path));
        data = File.ReadAllLines(path); //Read file
        if (data.Length == 0)
          throw new ArgumentException("File was empty");
        list = new LinkedList<int>();
        foreach (string line in data) //For every line in file data
          if (!int.TryParse(line, out int n)) //Try convert to int
            throw new Exception("File contains invalid number");
          else list.AddLast(n); //Add to linked list
        //Find middle, using follow leader technique, also print primes
        if (list.First == null) {
          Console.WriteLine("List is empty");
          return;
        }
        Console.Clear();
        Console.WriteLine($"\nDone [{i+1}]");

        follower = list.First; //Initiates at the head
        leader = list.First; //Initiates at the head
        primeCount = 0;
        while (leader != null) { //While leader is not null
          follower = follower.Next;
          leader = leader.Next;
          if (isPrime(leader.Value)) { //Print prime(s)
            if (primeCount == 0)
              Console.Write("Primes:");
            primeCount++;
            if (primeCount % 5 == 1)
              Console.Write($"\n [{leader.Value}]");
            else
              Console.Write($"{(primeCount % 5 == 1 ? "" : ",")}[{leader.Value}]");
          }
          if (leader != null) //If leader has not reached the end
            leader = leader.Next; //Increment again
        }
        if (primeCount > 0)
          Console.Write("\n");
        Console.WriteLine($"Middle: {follower.Value}");
      }
    }
    private static void binarySearchTreeTask() {
      string path;
      do {
        Console.Write("File path: ");
        path = Console.ReadLine();
        Console.Clear();
      } while (path.Length == 0 || !File.Exists(path));
      string[] lines;
      try {
        lines = File.ReadAllLines(path);
      } catch {
        Console.WriteLine("Failed to read file");
        return;
      }
      BST tree = new BST();
      string[] numbers;
      int num;
      foreach (string line in lines) {
        numbers = line.Split(' ');
        foreach (string number in numbers) {
          if (!int.TryParse(number, out num)) {
            Console.Clear();
            Console.WriteLine($"Error reading file: {path}");
            return;
          }
          tree.add(num);
        }
      }
      if (tree.root == null) {
        Console.WriteLine("Tree is empty.");
        return;
      }
      Console.Clear();
      Queue<BSTNode> qu = new Queue<BSTNode>();
      qu.Enqueue(tree.root);

      int layerCount = 0;
      int counter = 1;
      BSTNode current;
      int nextCounter = 0;
      //Print by level
      do {
        current = qu.Dequeue();
        counter--;
        if (current.left != null) {
          nextCounter++;
          qu.Enqueue(current.left);
        }
        if (current.right != null) {
          nextCounter++;
          qu.Enqueue(current.right);
        }
        if (counter == 0) {
          Console.Write($"\nLayer {layerCount}: [{current.value}]");
          layerCount++;
          counter = nextCounter;
          nextCounter = 0;
        } else 
          Console.Write($",[{current.value}]");
      } while (qu.Count != 0);

      //Get primes
      qu = new Queue<BSTNode>();
      qu.Enqueue(tree.root);
      int primeCount = 0;
      layerCount = 0;
      counter = 1;
      nextCounter = 0;
      do {
        current = qu.Dequeue();
        counter--;
        if (current.left != null) {
          nextCounter++;
          qu.Enqueue(current.left);
        }
        if (current.right != null) {
          nextCounter++;
          qu.Enqueue(current.right);
        }
        if (counter == 0) {
          layerCount++;
          counter = nextCounter;
          nextCounter = 0;
        }
        if (isPrime(current.value)) {
          if (++primeCount == 1)
            Console.Write("\nPrimes:");
          if (primeCount % 5 == 1)
            Console.Write($"\n [{current.value}]");
          else
            Console.Write($"{(primeCount % 5 == 1 ? "" : ",")}[{current.value}]");
        }
      } while (qu.Count != 0);
      Console.ReadKey();
    }
    private static void hashtableTask() {
      string path = "";
      string[] lines = null;
      do {
        Console.Write("Enter file path: ");
        path = Console.ReadLine();
        Console.Clear();
      } while (path.Length == 0 || !File.Exists(path));
      Hashtable stringHT = new Hashtable();
      Hashtable charHT = new Hashtable();

      lines = File.ReadAllLines(path);
      string[] words;
      for (int i = 0; i < lines.Length; i++) {
        words = lines[i].Split(' ');
        foreach (string word in words) {
          if (!stringHT.ContainsKey(word)) //If string isn't in table
            stringHT.Add(word, 1); //Add string with count of 1 
          else
            stringHT[word] = (int)stringHT[word] + 1; //Otherwise increment count
          foreach (char c in word) //For every character in string
            if (!charHT.ContainsKey(c)) //If character isnt in hashtable
              charHT.Add(c, 1); //Add it with count of 1
            else charHT[c] = (int)charHT[c] + 1; //Otherwise increment
        }
      }
      
      int highest = 0;
      string highestString = "";
      char highestChar = (char)0;
      //Gets string frequency
      foreach (string key in stringHT.Keys) //O(n)
        //Find highest
        if ((int)stringHT[key] > highest) {
          highest = (int)stringHT[key];
          highestString = key;
        }
      Console.WriteLine($"Most popular string: '{highestString}'");

      highest = 0;
      //Gets character frequency
      foreach (char key in charHT.Keys)
        //Find highest
        if ((int)charHT[key] > highest) {
          highest = (int)charHT[key];
          highestChar = key;
        }
      Console.WriteLine($"Most popular character: '{highestChar}'");
    }
    private static bool isPrime(int n) {
      if (n < 2)
        return false;
      int sqrt = (int)Math.Sqrt(n);
      //Loops through 2 - Square root of given number
      for (int i = 2; i <= sqrt; i++)
        if (n % i == 0) //If number divisible by i == 0 return false
          return false;
      return true;
    }
  }
}