#include "BinaryTree.h"
#include "raylib.h"
#include <iostream>
#include <sstream>

template <typename T>
BinaryTree<T>::Node::Node(T val) {
	data = val;
	left = nullptr;
	right = nullptr;
}

template <typename T>
void BinaryTree<T>::Node::AddLeft(Node* node) {
	left = node;
}

template <typename T>
void BinaryTree<T>::Node::AddRight(Node* node) {
	right = node;
}

template <typename T>
T BinaryTree<T>::Node::GetData() {
	return data;
}

template <typename T>
void BinaryTree<T>::Node::ChangeData(T val) {
	data = val;
}

template <typename T>
Node* BinaryTree<T>::Node::GetLeft() {
	return left;
}

template <typename T>
Node* BinaryTree<T>::Node::GetRight() {
	return right;
}

template<typename T>
bool BinaryTree<T>::Node::IsGreater(T x) {
	return data > x;
}

template <typename T>
bool BinaryTree<T>::Node::IsGreater(std::string x) {
	return data.compare(x) > 0;
}

template<typename T>
bool BinaryTree<T>::Node::Equals(T x) {
	return data == x;
}

template <typename T>
bool BinaryTree<T>::Node::Equals(std::string x) {
	return data.compare(x) == 0;
}

template <typename T>
void BinaryTree<T>::Node::Destroy() {
	delete(this);
}

template <typename T>
void BinaryTree<T>::Node::Draw(int x, int y, float radius) {
	DrawCircleLines(x + (int)radius, y + (int)radius, radius, Color BLACK);
	DrawText(std::to_string(data).c_str(), x + (int)radius, y + (int)radius, (int)radius / 3, Color BLACK);
}

template <typename T>
BinaryTree<T>::BinaryTree() {
root = nullptr;
}

template <typename T>
void BinaryTree<T>::Insert(T val) {
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

template <typename T>
void BinaryTree<T>::Delete(T val) {
	if (root == nullptr) {
		return;
	}
	Node* prev;
	Node* curr = root;
	while (curr != nullptr && !curr->Equals(val)) {
		prev = curr;
		if (curr->IsGreater(val)) {
			curr = curr->GetLeft();
		}
		else {
			curr = curr->GetRight();
		}
	}
	if (curr->GetLeft() == nullptr && curr->GetRight() == nullptr) {
		if (prev->IsGreater(curr->GetData())) {
			prev->AddLeft(nullptr);
		}
		else {
			prev->AddRight(nullptr);
		}
		curr->Destroy();
	}
	else if (curr->GetLeft() == nullptr || curr->GetRight() == nullptr) {
		Node* child = curr->GetLeft();
		if (child == nullptr) {
			child = curr->GetRight();
		}
		if (prev->IsGreater(curr->GetData())) {
			prev->AddLeft(child);
		}
		else {
			prev->AddRight(child);
		}
		curr->Destroy();
	}
	else {
		Node* next = curr->GetRight();
		prev = curr;
		while (next->GetLeft() != nullptr) {
			prev = next;
			next = next->GetLeft();
		}
		curr->ChangeData(next->GetData());
		prev->AddLeft(next->GetRight());
		next->Destroy();
	}
}

template <typename T>
Node* BinaryTree<T>::Search(T val) {
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

template <typename T>
void BinaryTree<T>::Draw() {
	if (root == nullptr) {
		return;
	}
	Node* curr = root;
	DrawNodes(curr, GetScreenWidth() / 2, 0);
}

template <typename T>
Node* BinaryTree<T>::CreateNode(T val) {
	return new Node(val);
}

template <typename T>
void BinaryTree<T>::DrawNodes(Node* curr, int x, int y) {
	curr->Draw(x, y, (float)(GetScreenHeight() / (height * 2)));
	if (curr->GetLeft() != nullptr) {
		DrawNodes(curr->GetLeft(), x - GetScreenHeight() / height, y + GetScreenHeight() / height);
	}
	if (curr->GetRight() != nullptr) {
		DrawNodes(curr->GetRight(), x + GetScreenHeight() / height, y + GetScreenHeight() / height);
	}
}