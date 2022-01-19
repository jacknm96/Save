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
    }

    int Count() {
        Node* next = head;
        int count = 0;
        while (next != nullptr) {
            next = next->GetNext();
            count++;
        }
        return count;
    }

    bool IsEmpty() {
        return head == nullptr;
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
    }

private:

    Node* head;
    Node* tail;

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
    std::cout << list.Count() << std::endl;
    std::cout << list.GetFront().GetData() << std::endl;
    std::cout << list.GetBack().GetData() << std::endl;
    list.PopFront();
    std::cout << list.GetFront().GetData() << std::endl;
    list.PopBack();
    std::cout << list.GetBack().GetData() << std::endl;
    list.PopBack();
    std::cout << list.GetBack().GetData() << std::endl;
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
