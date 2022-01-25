// DoubleLinkedList.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

template<typename T>
class DoubleLinkedList {

public:
    // Defines each node of the list
    class Node {
    public:
        Node(T val) {
            data = val;
            prev = nullptr;
            next = nullptr;
        }

        void AddNext(Node* node) {
            next = node;
        }

        void AddPrev(Node* node) {
            prev = node;
        }

        T GetData() {
            return data;
        }

        Node* GetNext() {
            return next;
        }

        Node* GetPrev() {
            return prev;
        }

        // returns true if Node's data is greater than passed parameter
        template<typename T>
        bool IsGreater(T x) {
            return data > x;
        }

        // string override
        bool IsGreater(std::string x) {
            return data.compare(x) > 0;
        }

        void Destroy() {
            delete(this);
        }

    private:
        T data;
        Node* prev;
        Node* next;
    };

    DoubleLinkedList() {
        head = nullptr;
        tail = nullptr;
        count = 0;
    }

    int Count() {
        return count;
    }

    bool IsEmpty() {
        return count == 0;
    }

    // returns the head node in list
    Node GetFront() {
        if (head != nullptr) {
            return *head;
        }
        else {
            return NULL;
        }
    }

    // returns last node in list
    Node GetBack() {
        if (tail != nullptr) {
            return *tail;
        }
        else {
            return NULL;
        }
    }

    // creates new node using passed parameter and pushes it to front
    void PushFront(T val) {
        if (head == nullptr) {
            head = CreateNode(val);
            tail = head;
        }
        else {
            Node* holder = CreateNode(val);
            holder->AddNext(head);
            head->AddPrev(holder);
            head = holder;
        }
        count++;
    }

    // deletes front node;
    void PopFront() {
        if (head != nullptr) {
            if (head == tail) {
                head = nullptr;
                tail = nullptr;
            }
            else {
                Node* temp = head;
                head = head->GetNext();
                temp->Destroy();
            }
            count--;
        }
    }

    // deletes back node
    void PopBack() {
        if (tail != nullptr) {
            if (head == tail) {
                head = nullptr;
                tail = nullptr;
            }
            else {
                Node* temp = tail;
                tail = tail->GetPrev();
                temp->Destroy();
            }
            count--;
        }
    }

    // creates new node using passed parameter and pushes it to tail
    void PushBack(T val) {
        if (tail == nullptr) {
            head = CreateNode(val);
            tail = head;
        }
        else {
            Node* holder = CreateNode(val);
            holder->AddPrev(tail);
            tail->AddNext(holder);
            tail = holder;
        }
        count++;
    }

    // creates new node using passed parameter and inserts it at given index (head = 0)
    void Insert(T val, int index) {
        if (index > count || index < 0) { // if passed index is outside bound of list
            return;
        }
        if (index == count) { // if index is last item, push back
            PushBack(val);
            return;
        }
        else if (index == 0) {// if index is first item, push front
            PushFront(val);
            return;
        }
        Node* curr = head;
        int counter = index;
        while (counter > 0) { // iterate through list up to index
            curr = curr->GetNext();
            counter--;
        }
        Node* holder = CreateNode(val);
        Node* prev = curr->GetPrev();
        if (prev != nullptr) {
            prev->AddNext(holder);
        }
        holder->AddNext(curr);
        holder->AddPrev(prev);
        curr->AddPrev(holder);
        count++;
    }

    // deletes node containing passed value. if multiple nodes have the same value, deletes the first one
    void Delete(T val) {
        Node* curr = head;
        while (curr != nullptr && curr->GetData() != val) {
            curr = curr->GetNext();
        }
        if (curr == nullptr) {
            return;
        }
        Node* prev = curr->GetPrev();
        Node* next = curr->GetNext();
        if (next != nullptr) {
            next->AddPrev(prev);
        }
        else {
            tail = prev;
        }
        if (prev != nullptr) {
            prev->AddNext(next);
        }
        else {
            head = next;
        }
        curr->Destroy();
        count--;
    }

    // prints out list
    void PrintList() {
        Node* next = head;
        if (next != nullptr) {
            std::cout << next->GetData();
            next = next->GetNext();
        }
        while (next != nullptr) {
            std::cout << " <-> " << next->GetData();
            next = next->GetNext();
        }
        std::cout << std::endl;
    }

    // sorts list using bubble sort method
    void BubbleSort()
    {
        if (head == nullptr) { // return if empty list
            return;
        }
        bool clean = false; // will check to see if the array is sorted yet
        while (!clean) // while array is not sorted
        {
            clean = true; // prime to assume it is sorted
            Node* curr = head;
            Node* next = curr->GetNext();
            while (next != nullptr) { // while we havent reached end of list
                if (curr->IsGreater(next->GetData())) { // reassign pointers to swap 2 nodes
                    if (curr == head) {
                        head = next;
                    }
                    else {
                        curr->GetPrev()->AddNext(next);
                    }
                    if (next == tail) {
                        tail = curr;
                    }
                    else {
                        next->GetNext()->AddPrev(curr);
                    }
                    curr->AddNext(next->GetNext());
                    next->AddPrev(curr->GetPrev());
                    curr->AddPrev(next);
                    next->AddNext(curr);
                    next = curr->GetNext();
                    clean = false;
                }
                else {
                    curr = next;
                    next = curr->GetNext();
                }
            }
        }
    }

private:

    Node* head;
    Node* tail;
    int count;

    static Node* CreateNode(T val) {
        return new Node(val);
    }
};

int main()
{
    DoubleLinkedList<std::string> list = DoubleLinkedList<std::string>();
    list.PushFront("hi");
    list.PushBack("poop");
    list.Insert("class", 1);
    list.PushBack("c++ sucks");
    list.Insert("c# is better", 3);
    list.PushFront("potato");
    list.PrintList();
    list.BubbleSort();
    list.PrintList();
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
