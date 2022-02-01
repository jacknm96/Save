#pragma once

template <typename T>
class BinaryTree {
public:
	class Node { //defines our node objects in our tree
	public:
		Node(T val);

		void AddLeft(Node* node); //adds a left child

		void AddRight(Node* node); //adds a right child

		T GetData(); //returns the data in node

		void ChangeData(T val); //changes the data in node - used for deletion to copy data

		Node* GetLeft(); //returns pointer to left child

		Node* GetRight(); //returns pointer to right child

		bool IsGreater(T x); //returns true if greater than given data

		bool Equals(T x); //returns true if equals given data

		void Destroy(); //deletes this node

		void Draw(int x, int y, float radius); //draws this node in raylib

	private:
		T data;
		Node* left;
		Node* right;
	};

	BinaryTree();

	void Insert(T val); //inserts given value in ordered binary tree

	void Delete(T val); //deletes value if it exists in tree and reorders tree accordingly

	Node* Search(T val); //returns node containing the given value

	void Draw(); //draws the binary tree in raylib

private:
	Node* root;

	Node* CreateNode(T val);

	void DrawNodes(Node* curr, int x, int y, float radius);
};
