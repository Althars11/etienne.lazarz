# -*- coding: utf-8 -*-
"""Binary Tree module"""

from .queue import Queue


class BinTree:
    """Simple class for binary tree

    Attributes:
        key (Any): Node key.
        left (BinTree): Left child.
        right (BinTree): Right child.

    """

    def __init__(self, key, left = None, right = None):
        """Init binary tree.

        Args:
            key (Any): Node key.
            left (BinTree): Left child.
            right (BinTree): Right child.

        """

        self.key = key
        self.left = left
        self.right = right


def size(ref):
    """Compute size of tree.

    Args:
        ref (BinTree).

    Returns:
        int: The number of nodes of tree.

    """

    if ref == None:
        return 0
    else:
        return 1 + size(ref.left) + size(ref.right)


def height(ref):
    """Compute height of Tree.

    Args:
        ref (BinTree).

    Returns:
        int: The maximum depth of any leaf.

    """

    if ref == None:
        return -1
    else:
        return 1 + max(height(ref.left), height(ref.right))



def _deserialize_content(content, pos, end, elttype=int):
    """Produce a BinTree from input string.

    Args:
        content (str): String to parse as BinTree.
        pos (int): Current character index in `content`.
        end (int): Length of `content`.

    Returns:
        (BinTree, int): BinTree parsed and next character index to use.

    Raises:
        ExempleSyntaxError: If any format error is encountered.

    """

    # Check missing content
    if pos >= end:
        raise Exception("Unexpected end of content")
    # Check and skip opening parenthesis
    if content[pos] != '(':
        raise Exception("Missing '(' in exemple data")
    pos += 1
    # Check for empty tree
    if content[pos] == ')':
        return None, pos + 1
    # Get BinTree key and create node
    key = ""
    while pos < end and "(" != content[pos] and ")" != content[pos]:
        key += content[pos]
        pos += 1
#    if key == "":
#        raise Exception("Empty key")
    tree = BinTree(elttype(key), None, None)
    # Parse children
    tree.left, pos = _deserialize_content(content, pos, end, elttype)
    tree.right, pos = _deserialize_content(content, pos, end, elttype)
    # Final tree and skip closing parenthesis
    if content[pos] != ')':
        raise Exception("Missing terminal ')' in exemple data")
    return tree, pos + 1


def load_bintree(path, elttype=int):
    """Load a BinTree from a text file.

    Args:
        path (str): Path to the text file.

    Returns:
        (BinTree): BinTree parsed from `path`.

    Raises:
        FileNotFoundError: If `path` is wrong.

    """

    # Open file and get full content
    file = open(path, 'r')
    content = file.read()
    # Remove all whitespace characters for easier parsing
    content = content.replace('\n', '').replace('\r', '') \
                     .replace('\t', '').replace(' ', '')
    # Parse content, close file and return tree
    tree, _ = _deserialize_content(content, 0, len(content), elttype)
    file.close()
    return tree

def __save(B):
    if B == None:
        return "()"
    else:
        return '(' + str(B.key) + __save(B.left) + __save(B.right) + ')'

def save(B, file_out):
    fout = open(file_out, mode='w')
    fout.write(__save(B))
    fout.close()
    

def dot_simple(ref):
    """Writes down dot format of binary tree.

    This is a simple implementation for quick demo and understanding.

    Args:
        ref (BinTree).

    Returns:
        str: String storing dot format of BinTree.

    """
    fake = 1
    s = "graph {\n" + 'graph [ordering="out"]\n'
    q = Queue()
    if ref:
        q.enqueue(ref)
        s += str(id(ref)) + '[label="' + str(ref.key) + '"]\n'
    while not q.isempty():
        node = q.dequeue()
       
        if node.left:
            s += str(id(node.left)) + '[label="' + str(node.left.key) + '"] \n'
            s += "   " + str(id(node)) + " -- " + str(id(node.left)) + "\n"
            q.enqueue(node.left)
        else:
            s += str(fake) + '[color="white" label=""] \n'
            s += "   " + str(id(node)) + " -- " + str(fake) + '[color="white"] \n'
            fake += 1
            
        if node.right:
            s += str(id(node.right)) + '[label="' + str(node.right.key) + '"] \n'
            s += "   " + str(id(node)) + " -- " + str(id(node.right)) + "\n"
            q.enqueue(node.right)
        else:
            s += str(fake) + '[color="white" label=""] \n'
            s += "   " + str(id(node)) + " -- " + str(fake) + '[color="white"] \n'
            fake += 1

    s += "}"
    return s

def display_simple(ref, eng=None):

    # Ensure all modules are available
    try:
        from graphviz import Source
        from IPython.display import display
    except:
        raise Exception("Missing module: graphviz.")

    display(Source(dot_simple(ref), engine=eng))
    


def _dot_build_matrix(tree_height):
    """Build an empty matrix for BinTree node spreading.

    Resulting matrix will be of dimension:
    - (2 ** tree_height) lines
    - (tree_height) columns
    Tree is stored as if drawn from left to right. This allows simpler matrix
    reduction in the next step.

    Args:
        tree_height (int): BinTree height.

    Returns:
        List[List[None]]: Empty matrix.

    """

    matrix = []
    for _ in range(2 ** (tree_height + 1)):
        row = []
        for _ in range(tree_height + 1):
            row.append(None)
        matrix.append(row)
    return matrix


def _dot_reduce_matrix(matrix):
    """Remove empty columns from given matrix.

    One empty column is kept when possible.

    Args:
        matrix (List[List[str]]): BinTree node keys spread as if on grid paper.

    Returns:
        List[List[str]]: Deep copy of `matrix`, excluding empty columns.

    """

    reduced_matrix = []
    empty_column_added = True
    j = 0
    while j < len(matrix):
        row = matrix[j]
        i = 0
        while i < len(matrix[0]) and matrix[j][i] == None:
            i += 1
        # Keep non-empty columns
        if i < len(matrix[0]):
            reduced_matrix.append(row)
            empty_column_added = False
        # Keep first empty columns for visual appeal
        elif empty_column_added == False:
            reduced_matrix.append(row)
            empty_column_added = True
        j += 1
    # Return reduced matrix in place of initial matrix
    return reduced_matrix

def _dot_from_content(content, shape="circle"):
    """Produce final dot source from body lines.

    Args:
        content (List[str]): Lines to add to dot output.

    Returns:
        str: Full dot for BinTree.

    """

#        node [shape="''' + shape + '''" , fixedsize=true, height=0.4, width=0.4];
    res = '''graph G {
        layout="neato";
        node [shape="''' + shape + '''"];
        edge [arrowhead=none];'''
    for line in content:
        res += '\n        ' + line
    res += '\n}'
    return res

def dot(ref, wide_spread=False, shape="circle"):
    """Writes down dot format of binary tree.

    This is a "matrix" implementation used to spread nodes more evenly.
    It relies on a grid (``List[List[Tuple(BinTree, BinTree)]]``) and can
    put pressure on memory when tree gets high.

    Args:
        ref (BinTree).
        wide_spread (bool): Display whole tree width. Can get huge.

    Returns:
        str: String storing dot format of BinTree.

    """

    # Container for graph dot source
    graph_content = []
    # Traversal
    if ref:
        # Compute tree height
        tree_height = height(ref)
        # Init queue, and fill node list with:
        #  - node and parent references
        #  - hirearchical and level numbers
        q = Queue()
        q.enqueue((ref, None, 1, 1))
        nodes = []
        while not q.isempty():
            node, parent, hier, level = q.dequeue()
            nodes.append((node, parent, hier, level))
            if None != node.left:
                q.enqueue((node.left, node, 2 * hier, level + 1))
            if None != node.right:
                q.enqueue((node.right, node, 2 * hier + 1, level + 1))
        # Prepare matrix to spread nodes evenly as if on grid paper
        node_matrix = _dot_build_matrix(tree_height)
        # Put nodes in their corresponding matrix cell depending on node's
        # level and hierarchical number.
        for node, parent, hier, level in nodes:
            # Get node rank in level from hierarchical number
            rank = (hier - 2 ** (level - 1))
            # Compute level's first node horizontal offset and inter-node space
            offset = 2 ** (tree_height - level + 1) - 1
            sep_pow = tree_height - level + 2
            sep = 2 ** sep_pow
            # Node coordinates in matrix space
            x = offset + (rank) * (sep)
            y = -level
            # Fill matrix with node/parent tuple to display link later
            node_matrix[x][y] = (node, parent)
        # Reduce matrix by removing empty columns for more compact display
        if not wide_spread:
            node_matrix = _dot_reduce_matrix(node_matrix)
        # Add graph content from node matrix
        for i in range(len(node_matrix)):
            for j in range(len(node_matrix[0])):
                if None != node_matrix[i][j]:
                    node, parent = node_matrix[i][j]
                    x = i * 0.25
                    y = j / 1.5
                    node_dot = 'N{}[label="{}";pos="{},{}!"];'
                    node_dot = node_dot.format(id(node), node.key, x, y)
                    graph_content.append(node_dot)
                    if None != parent:
                        edge_dot = 'N{} -- N{};'.format(id(parent), id(node))
                        graph_content.append(edge_dot)
    # Build graph and return
    return _dot_from_content(graph_content, shape)


def display(ref, *args, **kwargs):
    """Render a BinTree to for in-browser display.

    *Warning:* Made for use within IPython/Jupyter only.

    Extra non-documented arguments are passed to the ``dot`` function and
    complyt with its documentation.

    Args:
        ref (BinTree).

    Returns:
        Source: Graphviz wrapper object for BinTree rendering.

    """

    # Ensure all modules are available
    try:
        from graphviz import Source
        from IPython.display import display
    except:
        raise Exception("Missing module: graphviz.")
    # Generate dot and return display object
    dot_source = dot(ref, *args, **kwargs)
#    return Source(dot_source)
    display(Source(dot_source))
    


#  ASCII display


def printer(root, digits=2):
    """
    display in ASCII the tree root
    digits: maximum nb digits of keys (when int)
    """
    nodes = [root]
    level = 1
    maxLevel = height(root) + 1 + digits // 2
    while level <= (maxLevel-digits//2):
        floor = maxLevel - level
        edgeLines = 2 ** (max(floor - 1, 0))
        firstSpaces = 2 ** floor - 1
        betweenSpaces = 2 ** (floor + 1) - 1
        print(' ' * (firstSpaces - digits // 2), end='')
        newNodes = []
        for node in nodes:
            if node != None:
                if node.key:
                    print(node.key, end='')
                else:
                    print('.', end='')
                newNodes.append(node.left)
                newNodes.append(node.right)
            else:
                newNodes.append(None)
                newNodes.append(None)
                print(' ' * digits, end='')
            print(' ' * (betweenSpaces-digits+1), end='')
        print()
        if level < (maxLevel-digits//2):
            for i in range(1, edgeLines + 1):
                for node in nodes:
                    print(' ' * (firstSpaces - i), end='')
                    if node == None:
                        print(' ' * (2 * edgeLines + i + 1), end='')
                    else:
                        if node.left != None:
                            print('/', end='')
                        else:
                            print(' ', end='')
                        print(' ' * (2 * i - 1), end='')
                        if node.right != None:
                            print('\\', end='')
                        else:
                            print(' ', end='')
                        if node != nodes[-1]:
                            print(' ' * (2 * edgeLines - i), end='')
                print()
        nodes = newNodes
        level += 1
