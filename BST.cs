using System;

namespace DataStructures_Assignment {
  class BST {
    public BSTNode root = null;
    public BST(int rootValue) {
      root = new BSTNode(rootValue);
    }
    public BST() { }
    public BST(int[] values) {
      foreach (int value in values) //Loop through all adding values
        add(value); //Add them
    }
    public void add(int[] values) {
      foreach (int value in values) //Loop through all adding values
        add(value); //Add them
    }
    public BSTNode delete(int value, bool useLeft = true) {
      return delete(get(value), useLeft);
    }
    public BSTNode delete(BSTNode node, bool useLeft = true) {
      if (node == null) return null; //If node is null. Return null
      BSTNode replacingNode;

      replacingNode = (useLeft) ? getMax(node.left) : getMin(node.right); //Use correct selection algorithm

      delete(replacingNode, useLeft); //Recursively delete the replacing node

      if (node == root) //If node is the root. Replace the root with replacing node
        return root = replacingNode;

      BSTNode parent = getParent(node.value); //Get the parent

      //Find replacing node
      if (node.value > parent.value)
        parent.right = replacingNode;
      else
        parent.left = replacingNode;

      return parent; //Return the parent
    }
    public BSTNode add(int value) {
      if (root == null) //If root is null. Create one with given value
        return root = new BSTNode(value);
      BSTNode current = root;
      while (true) { //Add node iteratively
        if (value > current.value) { //If node belongs on the right. Attempt to add
          if (current.right == null) //If node.right is null. Set it to new node
            return current.right = new BSTNode(value);
          else //Otherwise attempt to add to node.right
            current = current.right;
        } else if (value < current.value) { //If node belongs on the left. Attempt to add
          if (current.left == null) //If node.left is null, set it to new node.
            return current.left = new BSTNode(value);
          else current = current.left; //Otherwise attempt to add to node.left
        } else return current; //If adding value is equal to current node. Return as no dupes are allowed
      }
    }
    public BSTNode getParent(BSTNode node) {
      return getParent(node.value);
    }
    public BSTNode getParent(int value) {
      BSTNode node = root;
      //Loop till offspring contains the given value
      while (node != null && node.left.value != value || node.right.value != value) 
        node = (value > node.value) ? node.right : node.left;
      return node; //Return it. Even if it's null
    }
    public BSTNode get(int value) {
      BSTNode node = root;
      while (node != null && node.value != value) //Loop till you find the node with given value
        node = (value < node.value) ? node.left : node.right;
      return node;
    }
    private BSTNode getMin(BSTNode node = null) {
      if (root == null) return null;
      if (node == null) node = root;
      while (node.left != null) //Continuously go left
        node = node.left;
      return node;
    }
    private BSTNode getMax(BSTNode node = null) {
      if (root == null) return null;
      if (node == null) node = root;
      while (node.right != null) //Continously go right
        node = node.right;
      return node;
    }
    public int getDepth(int value) {
      return getDepth(value, out BSTNode node);
    }
    public int getDepth(int value, out BSTNode node) {
      node = root;
      int depth = 0;
      while (node != null && node.value != value) { //Count up the depth to a node
        node = (value < node.value) ? node.left : node.right;
        depth++;
      }
      return (node == null) ? -1 : depth; //If node wasn't found. Return -1 otherwise return depth
    }
    public int getHeight(int value) {
      return getHeight(get(value));
    }
    public static int getHeight(BSTNode node) {
      if (node == null) //If node is null, return -1
        return -1;
      if (node.left == null && node.right == null) //If there are no siblings. You have reached 0 height
        return 0;
      else if (node.left != null ^ node.right == null) //Otherwise find deepest sub tree and return height from bottom
        return (node.left != null) ? getHeight(node.left) + 1 : getHeight(node.right) + 1;
      else
        return Math.Max(getHeight(node.left), getHeight(node.right)) + 1;
    }
  }
}