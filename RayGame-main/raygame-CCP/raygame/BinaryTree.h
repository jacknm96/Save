#pragma once
#include <iostream>

template <typename T>
class BinaryTree {
public:
	class Node {
	public:
		Node(T val);

		void AddLeft(Node* node);

		void AddRight(Node* node);

		T GetData();

		void ChangeData(T val);

		Node* GetLeft();

		Node* GetRight();

		bool IsGreater(T x);

		bool IsGreater(std::string x);

		bool Equals(T x);

		bool Equals(std::string x);

		void Destroy();

		void Draw(int x, int y, float radius);

	private:
		T data;
		Node* left;
		Node* right;
	};

	BinaryTree();

	void Insert(T val);

	void Delete(T val);

	Node* Search(T val);

	void Draw();

private:
	Node* root;

	Node* CreateNode(T val);

	void DrawNodes(Node* curr, int x, int y);
};
