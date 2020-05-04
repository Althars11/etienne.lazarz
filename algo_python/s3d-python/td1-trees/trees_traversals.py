#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
S3: trees
traversals: DFS and BFS
"""

from algopy import tree, treeasbin, queue

# DFS (Depth-First Search)

def DFS(T):
    """
    T: Tree
    """
    if T.nbchildren == 0: #T.children == []
        print("leaf:", T.key)
    else:
        print("prefix:", T.key)
        for i in range(T.nbchildren - 1):
            DFS(T.children[i])
            # inter
        DFS(T.children[-1]) # T.children[T.nbchildren-1]
        print("suffix:", T.key)

def DFS_bin(B):
    """
    B: TreeAsBin
    """
    if B.child == None:
        print("leaf:", B.key)
    else:
        print("prefix:", B.key)
        C = B.child
        while C.sibling != None:
            DFS_bin(C)
            C = C.sibling
        DFS_bin(C)
        print("suffix:", B.key) 
    
#BFS (Breadth First Search)

def BFS(T):
    """
    T: Tree
	displays keys, one linel per level
    """
    q = queue.Queue()
    q.enqueue(T)
    q.enqueue(None)
    while not q.isempty():
        T = q.dequeue()
        if T == None:
            print()
            if not q.isempty():
                q.enqueue(None)
        else:
            print(T.key, end=' ')
            for C in T.children:
                q.enqueue(C)

def BFS_bin(B):
    """
    B: TreeAsBin 
	To modify: has to compute tree width
    """
    q = queue.Queue()
    q.enqueue(B)

    while not q.isempty():
        B = q.dequeue()
        C = B.child
        while C != None:
            q.enqueue(C)
            C = C.sibling










