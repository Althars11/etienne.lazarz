# -*- coding: utf-8 -*-
"""
S3: some wrong functions on binarytrees
"""

from algopy import bintree

def searchBST(x, B):
    if B == None:
        return None
    elif x == B.key:
        return B
    else:
        if x < B.key:
            searchBST(x, B.left)
        else:
            searchBST(x, B.right)

def insertBST(x, B):
    if B == None:
        return bintree.BinTree(x, None, None)
    else:
        if x == B.key:
            return B
        else:
            if x < B.key:
                return insertBST(x, B.left)
            else:
                return insertBST(x, B.right)
                
# after the copy, change a value in the initial tree and take a look at the copy!

def copy(B):
    if B == None:
        return binTree.BinTree(None, None, None)
    else:
        C = bintree.BinTree(B.key, B.left, B.right)
        copy(B.left)
        copy(B.right)
        return C
    
def copy2(B, C = None):
    if B == None:
        C = bintree.BinTree(None, None, None)
    else:
        C = bintree.BinTree(B.key, B.left, B.right)
        copy2(B.left, C.left)
        copy2(B.right, C.left)
        return C




                
