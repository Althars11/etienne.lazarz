#include <stdlib.h>
#include <stdio.h>
#include "list.h"

void list_init(struct list *list)
{
    list -> data = -2;
    list -> next = NULL;
}

int list_is_empty(struct list *list)
{
    return list_len(list)==0;
}

size_t list_len(struct list *list)
{
    size_t len;
    for (len = 0; list; list = list->next)
    {
        len += 1;
    }
    return len-1;
}

void list_push_front(struct list *list, struct list *elm)
{
    struct list *tmp = list -> next;
    list -> next = elm;
    elm -> next = tmp;
}

struct list *list_pop_front(struct list *list)
{
    struct list *tmp = list->next;
    if (tmp == NULL)
    {
        return tmp;
    }
    else
    {
        list -> next = tmp -> next;
        return tmp;
    }
}

struct list *list_find(struct list *list, int value)
{
    for (; list->data != value; list = list->next)
    {
        if (list -> next == NULL)
        {
            return NULL;
        }
    }
    return list;
}

struct list *list_lower_bound(struct list *list, int value)
{
    while (list -> next -> data < value)
    {
        if (list->next == NULL)
        {
            return NULL;
        }
        if (list->next->data >= value)
        {
            return list;
        }   
        list = list->next;
    }
}

int list_is_sorted(struct list *list)
{
    int is_sorted = 1;
    while (is_sorted && list->next != NULL)
    {
        if (list->data > list->next->data)
        {
            is_sorted = 0;
        }
        list = list->next;
    }
    return is_sorted;
}

void list_insert(struct list *list, struct list *elm)
{
    if (list->next == NULL)
    {
        list->next = elm;
        elm->next = NULL;
    }
    else
    {
        struct list *tmp = list;
        while (list->data < elm->data && list->next!=NULL)
        {
            list = list->next;
        }
        tmp = list-> next;
        list -> next = elm;
        elm -> next = tmp;
    }
}

void list_rev(struct list *list)
{
    if (list_len(list) > 1)
    {
        int len = list_len(list);
        struct list *debut = list;
        for (int i = 1 ; i < len; i++)
        {
            printf("i= %i\n",i);
            for (int j = 1; j<len; j++)
            { 
                struct list *tmp = list -> next;
                struct list *tmp2 = list;
                list = tmp;
                list->data = tmp -> data;
                list -> next = tmp2;
                list->next->data = tmp2 -> data;
                printf("j= %i\n",j);
                if (j<len-1)
                {
                    list = list->next;
                    printf("j<len-1\n");
                }
            }
            list = debut;
            list = list->next;
            printf("list=debut\n");
        }
        printf("went here");
    }
}

void list_half_split(struct list *list, struct list *second)
{
    int len = list_len(list);
    if (len>1)
    {
        for (size_t i = 0; i < len/2; i++)
        {
            list = list->next;
        }
        struct list *tmp = list->next;
        list->next=NULL;
        for (size_t i = 0; i < (len/2)-1; i++)
        {
            second -> next = tmp;
            tmp = tmp -> next;
        }
    }
}


//TEST


void print_list(struct list *list)
{
    printf("list_is_empty() = %s\n", list_is_empty(list) ? "yes" :  "no");
    printf("list_is_sorted() = %s\n", list_is_sorted(list) ? "yes" :  "no");
    printf("list_len() = %zu\n", list_len(list));

    int line = 1;

    printf("[");
    if (list->next)
    {
        goto pastfst;
        while (list->next)
        {
            line += printf(";");

            if (line > 72)
            {
                printf("\n ");
                line = 1;
            }

            pastfst:
            line += printf(" %2d", list->next->data);
            list = list->next;
        }
    }

    printf(" ]\n");
}

void list_insert_sort(struct list *list)
{
    if (list_is_empty(list))
        return;

    struct list fake_head = { 0, 0 };

    while (!list_is_empty(list))
    {
        struct list *elm = list_pop_front(list);
        list_insert(&fake_head, elm);
    }

    list->next = fake_head.next;
}

