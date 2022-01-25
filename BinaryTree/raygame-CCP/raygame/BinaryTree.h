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

		Node* GetLeft();

		Node* GetRight();

		template<typename T>
		bool IsGreater(T x);

		bool IsGreater(std::string x);

		template<typename T>
		bool Equals(T x);

		bool Equals(std::string x);

		void Destroy();

	private:
		T data;
		Node* left;
		Node* right;
	};

	DoubleLinkedList();

	void Insert(T val);

	void Delete(T val);

	Node Search(T val);

private:
	Node* root;

	Node* CreateNode(T val);
};
