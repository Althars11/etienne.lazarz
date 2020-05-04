from algopy import tree
from algopy.tree import Tree

"""
do not import this file in your login_prefixtrees.py 
-> this would prevent your code to be tested :(
only use it for your tests!
"""

################################################################################
## tree built with "textFiles/wordList1.txt"

Tree1 = Tree(('', False),
             [Tree(('c', False),
                   [Tree(('a', False),
                         [Tree(('s', False),
                               [Tree(('e', True)),
                                Tree(('t', True),
                                     [Tree(('l', False),
                                           [Tree(('e', True))])])])]),
                    Tree(('i', False),
                         [Tree(('r', False),
                               [Tree(('c', False),
                                     [Tree(('l', False),
                                           [Tree(('e', True))])])]),
                          Tree(('t', False),
                               [Tree(('y', True))])]),
                    Tree(('o', False),
                         [Tree(('m', False),
                               [Tree(('e', True))]),
                          Tree(('u', False),
                               [Tree(('l', False),
                                     [Tree(('d', True))])])])]),
        Tree(('f', False),
             [Tree(('a', False),
                   [Tree(('m', False),
                         [Tree(('e', True)),
                          Tree(('o', False),
                               [Tree(('u', False),
                                     [Tree(('s', True))])])]),
                    Tree(('n', True),
                         [Tree(('c', False),
                               [Tree(('y', True))])])])])])
                        