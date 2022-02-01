/*******************************************************************************************
*
*   raylib [core] example - basic window
*
*   This example has been created using raylib 1.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013-2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

#include "raylib.h"
#include "BinaryTree.h"
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
typename BinaryTree<T>::Node* BinaryTree<T>::Node::GetLeft() {
	return left;
}

template <typename T>
typename BinaryTree<T>::Node* BinaryTree<T>::Node::GetRight() {
	return right;
}

template<typename T>
bool BinaryTree<T>::Node::IsGreater(T x) {
	return data > x;
}

template<typename T>
bool BinaryTree<T>::Node::Equals(T x) {
	return data == x;
}

template <typename T>
void BinaryTree<T>::Node::Destroy() {
	delete(this);
}

template <typename T>
void BinaryTree<T>::Node::Draw(int x, int y, float radius) {
	DrawCircleLines(x + (int)radius, y + (int)radius, radius, BLACK);
	DrawText(std::to_string(data).c_str(), x + (int)radius, y + (int)radius, (int)radius / 3, BLACK);
}

template <typename T>
BinaryTree<T>::BinaryTree() {
	root = nullptr;
}

template <typename T>
void BinaryTree<T>::Insert(T val) {
	if (root == nullptr) { //if tree empty, simply create a root node
		root = CreateNode(val);
		return;
	}
	Node* curr = root;
	while (curr != nullptr) { // iterate through until we find an empty branch
		if (curr->IsGreater(val)) { //if value is <=, go left, otherwise go right
			if (curr->GetLeft() == nullptr) { //if left child empty, create child with value and end
				curr->AddLeft(CreateNode(val));
				break;
			}
			else {
				curr = curr->GetLeft();
			}
		}
		else if (curr->Equals(val)) { // no repeat values
			return;
		}
		else {
			if (curr->GetRight() == nullptr) { //if right child empty, create child with value and end
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
	if (root == nullptr) { //can't delete from empty tree
		return;
	}
	Node* prev = root; //prev keeps track of parent node to redirect pointers
	Node* curr = root;
	while (curr != nullptr && !curr->Equals(val)) { //iterate through list until we find value
		prev = curr;
		if (curr->IsGreater(val)) {
			curr = curr->GetLeft();
		}
		else {
			curr = curr->GetRight();
		}
	}
	if (curr == nullptr) { //value does not exist in tree
		return;
	}
	if (curr->GetLeft() == nullptr && curr->GetRight() == nullptr) { //value is leaf node
		if (prev->IsGreater(curr->GetData())) {
			prev->AddLeft(nullptr);
		}
		else {
			prev->AddRight(nullptr);
		}
		if (curr == root) {
			root = nullptr;
		}
		curr->Destroy();
	}
	else if (curr->GetLeft() == nullptr || curr->GetRight() == nullptr) { //value has a single child
		Node* child = curr->GetLeft();
		if (child == nullptr) {
			child = curr->GetRight();
		}
		if (curr == root) { //if value is root node, set child as new root
			root = child;
		}
		else {
			if (prev->IsGreater(curr->GetData())) {
				prev->AddLeft(child);
			}
			else {
				prev->AddRight(child);
			}
		}
		curr->Destroy();
	}
	else { //value has 2 children
		Node* next = curr->GetRight();
		prev = next;
		while (next->GetLeft() != nullptr) {
			prev = next;
			next = next->GetLeft();
		}
		curr->ChangeData(next->GetData());
		prev->AddLeft(next->GetRight());
		if (curr->GetRight() == next) {
			curr->AddRight(next->GetRight());
		}
		next->Destroy();
	}
}

template <typename T>
typename BinaryTree<T>::Node* BinaryTree<T>::Search(T val) {
	if (root == nullptr) { //cannot search empty tree
		return nullptr;
	}
	Node* curr = root;
	while (curr != nullptr && !curr->Equals(val)) { //iterate through tree until we find value
		if (curr->IsGreater(val)) {
			curr = curr->GetLeft();
		}
		else {
			curr = curr->GetRight();
		}
	}
	if (curr->Equals(val)) {
		return curr;
	}
	else {
		return nullptr;
	}
}

template <typename T>
void BinaryTree<T>::Draw() {
	if (root == nullptr) {
		return;
	}
	Node* curr = root;
	float radius = (float)(GetScreenHeight() / 8);
	DrawNodes(curr, GetScreenWidth() / 2 - radius, 0, radius);
}

template <typename T>
typename BinaryTree<T>::Node* BinaryTree<T>::CreateNode(T val) {
	return new Node(val);
}

template <typename T>
void BinaryTree<T>::DrawNodes(Node* curr, int x, int y, float radius) {
	curr->Draw(x, y, radius);
	if (curr->GetLeft() != nullptr) {
		DrawNodes(curr->GetLeft(), x - radius, y + radius * 2, radius / 2);
	}
	if (curr->GetRight() != nullptr) {
		DrawNodes(curr->GetRight(), x + radius * 2, y + radius * 2, radius / 2);
	}
}

int main()
{
	// Initialization
	//--------------------------------------------------------------------------------------
	int screenWidth = 800;
	int screenHeight = 450;

	InitWindow(screenWidth, screenHeight, "Binary Tree Exercise");

	SetTargetFPS(60);
	//--------------------------------------------------------------------------------------

	BinaryTree<int>* tree = new BinaryTree<int>();

	// Main game loop
	while (!WindowShouldClose())    // Detect window close button or ESC key
	{
		// Update
		std::cout << "Insert ('i'), delete ('d'), or escape('e')?" << std::endl;
		std::string input;
		std::cin >> input;
		if (input == "i") {
			std::cout << "Value to insert?" << std::endl;
			std::cin >> input;
			tree->Insert(std::stoi(input));
		}
		if (input == "d") {
			std::cout << "Value to delete?" << std::endl;
			std::cin >> input;
			tree->Delete(std::stoi(input));
		}
		if (input == "e") {
			break;
		}

		// Draw
		//----------------------------------------------------------------------------------
		BeginDrawing();

		ClearBackground(RAYWHITE);

		tree->Draw();

		EndDrawing();
		//----------------------------------------------------------------------------------
	}

	// De-Initialization
	//--------------------------------------------------------------------------------------   
	CloseWindow();        // Close window and OpenGL context
	//--------------------------------------------------------------------------------------

	return 0;
}