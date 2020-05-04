# -*- coding: utf-8 -*-
"""
Feb. 2020
B-trees - classics: search + insert + delete
"""

from algopy import btree

# 2.1: min (iterative) and max (recursive)

def minBTree(B):
    '''
    B is not empty
    '''
    while B.children != []:
        B = B.children[0]
    return B.keys[0]
    

def maxBTree(B):
    '''
    B is not empty
    '''
    if B.children:
        return maxBTree(B.children[B.nbkeys])  # B.children[-1]
    else:
        return B.keys[B.nbkeys-1]   # B.keys[-1]
    
# 2.2: Searching


def __binarySearchPos(L, x, left, right):
    """
    returns the position where x is or might be in L[left, right[
    """
    if right <= left:
        return right
    mid = left + (right - left) // 2
    if L[mid] == x:
        return mid
    elif x < L[mid]:
        return __binarySearchPos(L, x, left, mid)
    else:
        return __binarySearchPos(L, x, mid+1, right)    

def binarySearchPos(L, x):  # will be used in insertions and deletions
    """
    returns the position where x is or might be in L
    """
    return __binarySearchPos(L, x, 0, len(L))
    
def __searchBTree(B, x):                                       
    # FIXME
    pass

def searchBTree(B, x):
    if B == None:
        return None
    else:
        return __searchBTree(B, x)