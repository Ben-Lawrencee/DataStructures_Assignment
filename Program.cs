using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
namespace DataStructures_Assignment {
  class Program {
    static void Main(string[] args) {
      
      arrayTask();
      Console.ReadKey();
      doublyLinkedListTask();
      Console.ReadKey();
      binarySearchTreeTask();
      Console.ReadKey();
      hashtableTask();
      Console.ReadKey();
    }
    
    private static void arrayTask() {
      Console.Write("\nPath for Array Task: ");
      string path = Console.ReadLine();
      string[] data = File.ReadAllLines(path); //Get file data
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
      Console.Write("\nNumber of files: ");
      int numOfFiles = 0;
      //Get number of files
      while (true)
        if (!int.TryParse(Console.ReadLine(), out numOfFiles) && numOfFiles >= 0) { //If input is not a int or positive
          Console.Clear();
          Console.Write("Invalid input. Try again\nNumber of files: "); //Ask again
        } else break;
      
      Console.Clear();
      if (numOfFiles == 0) //Return if there are no files to enter
        return;
      string path;
      string[] data;
      LinkedList<int> list = new LinkedList<int>();
      LinkedListNode<int> follower;  
      LinkedListNode<int> leader;
      int primeCount;
      //Create linked lists from user entered files. Find middle, print primes
      for (int i = 0; i < numOfFiles; i++) { //
        Console.Write($"File [{i+1}], path: ");
        path = Console.ReadLine();
        while (!File.Exists(path)) { //Get path. Make sure it exists
          Console.Write("\nFile does not exist, try again: ");
          path = Console.ReadLine();
        }
        data = File.ReadAllLines(path); //Read file
        foreach (string line in data) //For every line in file data
          if (!int.TryParse(line, out int n)) //Try convert to int
            throw new Exception("File contains invalid number");
          else list.AddLast(n); //Add to linked list
        Console.Clear();
        Console.WriteLine($"\nDone [{i+1}]\n");

        //Find middle, using follow leader technique, also print primes
        follower = list.First; //Initiates at the head
        leader = list.First; //Initiates at the head
        primeCount = 0;
        while (leader != null) { //While leader is not null
          follower = follower.Next;
          leader = leader.Next;
          if (isPrime(leader.Value)) { //Print prime(s)
            if (primeCount == 0)
              Console.WriteLine("Primes: \n");
            primeCount++;
            if (primeCount % 5 == 0)
              Console.WriteLine($"[{leader.Value}]");
            else Console.Write($",[{leader.Value}]");
          }
          if (leader != null) //If leader has not reached the end
            leader = leader.Next; //Increment again
        }
        Console.WriteLine($"Middle: {follower.Value}");
      }
    }
    private static void binarySearchTreeTask() {
      string input = "";
      BST tree = new BST();
      while (input != "y") {
        Console.Clear();
        Console.Write("\nEnter 'y' when finsihed\nEnter number into binary search tree: ");
        input = Console.ReadLine();
        if (!int.TryParse(input, out int num)) {
          if (input == "y")
            break;
        } else tree.add(num);
      }
      Console.Clear();
      tree.printByLevel();
      tree.printPrimes();
    }
    private static void hashtableTask() {
      string input = "";
      Console.Write("\nProvide a exit key: ");
      string exitKey = Console.ReadLine();
      Console.Clear();
      Hashtable stringHT = new Hashtable();
      Hashtable charHT = new Hashtable();

      while (true) { //Iteratively ask, and add input's chars into Hashtable
        Console.Write($"\nExit key (Case sensitive): '{exitKey}'\nEnter a input: ");
        if (input == exitKey)
          break;
        if (stringHT[input] == null) //If string isn't in table
          stringHT.Add(input, 1); //Add string with count of 1 
        else stringHT[input] = (int)stringHT[input] + 1; //Otherwise increment count
        foreach (char c in input) //For every character in string
          if (charHT[c] == null) //If character isnt in hashtable
            charHT.Add(c, 1); //Add it with count of 1
          else charHT[c] = (int)charHT[c] + 1; //Otherwise increment
      }

      int highest = 0;
      string highestString = "";
      char highestChar = (char)0;
      //Gets string frequency
      foreach (string key in stringHT.Keys) {
        //Find highest
        if ((int)stringHT[key] > highest) {
          highest = (int)stringHT[key];
          highestString = key;
        }
      }

      highest = 0;
      //Gets character frequency
      foreach (char key in charHT.Keys)
        //Find highest
        if ((int)charHT[key] > highest) {
          highest = (int)charHT[key];
          highestChar = key;
        }

    }
    private static bool isPrime(int n) {
      if (n < 2)
        return false;
      bool prime = false;
      int sqrt = (int)Math.Sqrt(n);
      for (int i = 2; i < sqrt; i++)
        if (n % i == 0) {
          prime = true;
          break;
        }
      return prime;
    }
  }
}