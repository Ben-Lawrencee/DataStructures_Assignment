using System;
using System.Collections;

namespace DataStructures_Assignment {
  class BST {
    public BSTNode root = null;
    public BST(int rootValue) {
      root = new BSTNode(rootValue);
    }
    public BST() { }
    public BST(int[] values) {
      foreach (int value in values)
        add(value);
    }
    public void add(int[] values) {
      foreach (int value in values)
        add(value);
    }
    public BSTNode delete(int value, bool useLeft = true) {
      return delete(get(value), useLeft);
    }
    public BSTNode delete(BSTNode node, bool useLeft = true) {
      if (node == null) return null;
      BSTNode replacingNode;

      if (useLeft) replacingNode = getMax(node.left);
      else replacingNode = getMin(node.right);

      delete(replacingNode, useLeft);

      if (node == root)
        return root = replacingNode;

      BSTNode parent = getParent(node.value);

      if (node.value > parent.value)
        parent.right = replacingNode;
      else
        parent.left = replacingNode;

      return parent;
    }
    public BSTNode add(int value) {
      if (root == null)
        return root = new BSTNode(value);
      BSTNode node = root;
      while (node != null && !node.add(value))
        if (value > node.value) node = node.right;
        else node = node.left;
      return node;
    }
    public BSTNode getParent(BSTNode node) {
      return getParent(node.value);
    }
    public BSTNode getParent(int value) {
      BSTNode node = root;
      while (node != null && node.left.value != value || node.right.value != value)
        if (value > node.value) node = node.right;
        else node = node.left;
      return node;
    }
    public BSTNode get(int value) {
      BSTNode node = root;
      while (node != null && node.value != value)
        if (value > node.value) node = node.right;
        else node = node.left;
      return node;
    }
    private BSTNode getMin(BSTNode node = null) {
      if (root == null) return null;
      if (node == null) node = root;
      while (node.left != null)
        node = node.left;
      return node;
    }
    private BSTNode getMax(BSTNode node = null) {
      if (root == null) return null;
      if (node == null) node = root;
      while (node.right != null)
        node = node.right;
      return node;
    }
    public int getDepth(int value) {
      return getDepth(value, out BSTNode node);
    }
    public int getDepth(int value, out BSTNode node) {
      node = root;
      int depth = 0;
      while (node != null && node.value != value) {
        if (value < node.value)
          node = node.left;
        else
          node = node.right;
        depth++;
      }
      if (node == null)
        return -1;
      return depth;
    }
    public int getHeight(int value) {
      return getHeight(get(value));
    }
    public int getHeight(BSTNode node) {
      if (node == null)
        return -1;
      if (node.offSpring == 0)
        return 0;
      else if (node.offSpring == 1)
        if (node.left != null)
          return getHeight(node.left) + 1;
        else
          return getHeight(node.right) + 1;
      else
        return Math.Max(getHeight(node.left), getHeight(node.right)) + 1;
    }
    //Print Methods
    public void printByLevel() {
      if (root == null)
        return;
      Queue qu = new Queue ();
      qu.Enqueue(root);

      int layerCount = 0;
      int counter = 1;
      int nextCounter = 0;

      BSTNode current = root;
      BSTNode left = null;
      BSTNode right = null;

      while (current != null) {
        qu.Dequeue();
        counter--;
        left = current.left;
        right = current.right;
        if (left != null) {
          nextCounter++;
          qu.Enqueue(left);
        }
        if (right != null) {
          nextCounter++;
          qu.Enqueue(right);
        }
        if (counter == 0) {
          Console.Write($"\nLayer {layerCount}: [{current.value}]");
          layerCount++;
          counter = nextCounter;
          nextCounter = 0;
        } else
          Console.Write($",[{current.value}]");
        if (qu.Count == 0)
          break;
        current = (BSTNode)qu.Peek();
      }
      Console.WriteLine();
    }
    public void printPrimes() {
      Queue qu = new Queue ();
      qu.Enqueue(root);

      int layerCount = 0;
      int counter = 1;
      int nextCounter = 0;

      BSTNode current = root;
      BSTNode left = null;
      BSTNode right = null;

      while (current != null) {
        qu.Dequeue();
        counter--;
        left = current.left;
        right = current.right;
        if (left != null) {
          nextCounter++;
          qu.Enqueue(left);
        }
        if (right != null) {
          nextCounter++;
          qu.Enqueue(right);
        }
        if (counter == 0) {
          Console.Write($"\nLayer {layerCount}: [{current.value}]");
          layerCount++;
          counter = nextCounter;
          nextCounter = 0;
        } else
          Console.Write($",[{current.value}]");
        if (qu.Count == 0)
          break;
        current = (BSTNode)qu.Peek();
      }
    }
  }
}