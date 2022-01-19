// DoubleLinkedList.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

template<typename T>
class DoubleLinkedList {

public:
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

        template<typename T>
        bool IsLess(T &x) {
            return data < x;
        }

        template<>
        bool IsLess(std::string x) {
            return data.compare(x) < 0;
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

    Node GetFront() {
        if (head != nullptr) {
            return *head;
        }
        else {
            return NULL;
        }
    }

    Node GetBack() {
        if (tail != nullptr) {
            return *tail;
        }
        else {
            return NULL;
        }
    }

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
        }
        count--;
    }

    void PopBack() {
        if (head != nullptr) {
            if (head == tail) {
                head = nullptr;
                tail = nullptr;
            }
            else {
                Node* temp = tail;
                tail = tail->GetPrev();
                temp->Destroy();
            }
        }
        count--;
    }

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

    void Insert(T val, int index) {
        if (index > count || index < 0) {
            return;
        }
        if (index == count) {
            PushBack(val);
            return;
        }
        else if (index == 0) {
            PushFront(val);
            return;
        }
        Node* curr = head;
        int counter = index;
        while (counter > 0) {
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
    }

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
    }

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

    void BubbleSort()
    {
        bool clean = false; // will check to see if the array is sorted yet
        while (!clean) // while array is not sorted
        {
            clean = true; // prime to assume it is sorted
            Node* curr = head;
            Node* next = nullptr;
            if (curr->GetNext() != nullptr) {
                next = curr->GetNext();
            }
            while (next != nullptr) {
                if (curr->GetData() > next->GetData()) {

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
    DoubleLinkedList<int> list = DoubleLinkedList<int>();
    list.PushBack(6);
    list.PushFront(5);
    list.PopBack();
    list.PushBack(4);
    list.PushFront(3);
    list.PrintList();
    list.Insert(7, 2);
    list.PrintList();
    list.Delete(3);
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
