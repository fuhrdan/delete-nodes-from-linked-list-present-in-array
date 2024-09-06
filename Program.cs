//*****************************************************************************
//** 3217. Delete Nodes From Linked List Present in Array                    **
//*****************************************************************************


/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     struct ListNode *next;
 * };
 */
#define HASH_SIZE 100001

// Hash set implementation
struct HashSet {
    bool table[HASH_SIZE];
};

void insert(struct HashSet* set, int value) {
    set->table[value] = true;  // Mark the value as present in the hash set
}

bool contains(struct HashSet* set, int value) {
    return set->table[value];  // Check if the value exists in the hash set
}

// Optimized function to modify the linked list
struct ListNode* modifiedList(int* nums, int numsSize, struct ListNode* head) {
    struct HashSet numSet = { .table = {false} };  // Initialize the hash set

    // Insert all nums into the hash set
    for (int i = 0; i < numsSize; i++) {
        insert(&numSet, nums[i]);
    }

    // Dummy node to handle edge cases more easily
    struct ListNode* dummy = (struct ListNode*)malloc(sizeof(struct ListNode));
    dummy->next = head;
    
    struct ListNode* current = dummy;  // Start at dummy node

    while (current->next != NULL) {
        // If the value of next node is in nums, remove the node
        if (contains(&numSet, current->next->val)) {
            struct ListNode* nodeToRemove = current->next;
            current->next = nodeToRemove->next;  // Bypass the node
            free(nodeToRemove);  // Free the removed node
        } else {
            current = current->next;  // Move to the next node
        }
    }

    struct ListNode* newHead = dummy->next;  // The modified list's head
    free(dummy);  // Free the dummy node
    return newHead;
}

// Helper functions for testing
struct ListNode* createList(int* arr, int size) {
    struct ListNode* head = NULL;
    struct ListNode* temp = NULL;
    struct ListNode* current = NULL;

    for (int i = 0; i < size; i++) {
        temp = (struct ListNode*)malloc(sizeof(struct ListNode));
        temp->val = arr[i];
        temp->next = NULL;

        if (head == NULL) {
            head = temp;
            current = head;
        } else {
            current->next = temp;
            current = current->next;
        }
    }
    return head;
}