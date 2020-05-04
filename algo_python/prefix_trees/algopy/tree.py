# -*- coding: utf-8 -*-
"""
Class Tree for Prefix Trees homework
"""

# General Tree class
# ------------------------------------------------------------------------------

class Tree:
    """
    Simple class for General Tree
    """

    def __init__(self, key=None, children=None):
        """
        Init General Tree, children is [] if not given
        """
        self.key = key
        if children is not None:
            self.children = children
        else:
            self.children = []

    @property
    def nbchildren(self):
        return len(self.children)

    
