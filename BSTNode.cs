namespace DataStructures_Assignment {
  class BSTNode {
    public BSTNode left = null;
    public BSTNode right = null;
    public int offSpring {
      get {
        int count = 0;
        if (left != null)
          count++;
        else if (right != null)
          count++;
        return count;
      }
      set { }
    }
    public int value;
    public BSTNode(int value) {
      this.value = value;
    }
    public bool add(int value) {
      if (value == this.value)
        return false;
      else if (value > this.value) {
        if (right != null)
          return false;
        right = new BSTNode (value);
      } else {
        if (left != null)
          return false;
        left = new BSTNode (value);
      }
      return true;
    }
  }
}