#include "BinaryTree.h"
#include <iostream>

template <typename T>
class BinaryTree {
public:
	class Node {
	public:
		Node(T val) {
			data = val;
			left = nullptr;
			right = nullptr;
		}

		void AddLeft(Node* node) {
			left = node;
		}

		void AddRight(Node* node) {
			right = node;
		}

		T GetData() {
			return data;
		}

		Node* GetLeft() {
			return left;
		}

		Node* GetRight() {
			return right;
		}

		template<typename T>
		bool IsGreater(T x) {
			return data > x;
		}

		bool IsGreater(std::string x) {
			return data.compare(x) > 0;
		}

		template<typename T>
		bool Equals(T x) {
			return data == x;
		}

		bool Equals(std::string x) {
			return data.compare(x) == 0;
		}

		void Destroy() {
			delete(this);
		}

	private:
		T data;
		Node* left;
		Node* right;
	};

	DoubleLinkedList() {
		root = nullptr;
	}

	void Insert(T val) {
		if (root == nullptr) {
			root = CreateNode(val);
			return;
		}
		Node* curr = root;
		while (curr != nullptr) {
			if (curr->IsGreater(val)) {
				if (curr->GetLeft() == nullptr) {
					curr->AddLeft(CreateNode(val));
					break;
				}
				else {
					curr = curr->GetLeft();
				}
			}
			else {
				if (curr->GetRight() == nullptr) {
					curr->AddRight(CreateNode(val));
					break;
				}
				else {
					curr = curr->GetRight();
				}
			}
		}
	}

	void Delete(T val);

	Node* Search(T val) {
		if (root == nullptr) {
			return;
		}
		Node* curr = root;
		while (curr != nullptr && !curr->Equals(val)) {
			if (curr->IsGreater(val)) {
				curr = curr->GetLeft();
			}
			else {
				curr = curr->GetRight();
			}
		}
		return curr;
	}

private:
	Node* root;

	Node* CreateNode(T val) {
		return new Node(val);
	}
};