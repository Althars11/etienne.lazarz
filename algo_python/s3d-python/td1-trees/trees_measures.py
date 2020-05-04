# -*- coding: utf-8 -*-
"""
S3: trees
measures
"""

from algopy import tree, treeasbin

def size(T):
    """
    T: Tree
    return T's size
    """
    s = 1
    for C in T.children:
        s += size(C)
    return s
    
def size_bin(B):
    """
    B: TreeAsBin
    return B's size
    """
    s = 1
    C = B.child
    while C != None:
        s += size_bin(C)
        C = C.sibling
    return s
    
def size_bin2(B):
    if B == None:
        return 0
    else:
        return 1 + size_bin2(B.child) \
                 + size_bin2(B.sibling)

def size_bin3(B):
    return 1 + (size_bin2(B.child) if B.child else 0) \
             + (size_bin2(B.sibling) if B.sibling else 0)
     
def height(T):
    """
    T: Tree
    return T's height
    """
    h = -1
    for i in range(T.nbchildren):
        h = max(h, height(T.children[i]))
    return h + 1

def height_bin(B):
    """
    B: TreeAsBin
    return B's height
    """
    h = -1
    C = B.child
    while C:
        h = max(h, height_bin(C))
        C = C.sibling
    return h + 1
    
def height_bin2(B):
    if B == None:
        return -1
    else:
        return max(1 + height_bin2(B.child),
                   height_bin2(B.sibling))
    
    









    
    
    