__license__ = 'Nathalie (c) EPITA'
__docformat__ = 'reStructuredText'
__revision__ = '$Id: prefixtrees.py 2020-02-14'

"""
Prefix Trees homework
2020-02 - S3#
@author: etienne.lazarz
"""

from algopy import tree

###############################################################################
# Do not change anything above this line, except your login!
# Do not add any import



##############################################################################
## Measure

def countwords(T):
    """ count words in tree T
    
    """
    compteur = 0;
    for C in T.children:
            (lettre,isword) = C.key
            if(isword==True):
                compteur+=1
    return compteur
                
    

def longestwordlength(T):
    """ longest word length
    
    """
    longest_word = 1
    for i in range (T.children):
        longest_word = max(longest_word(T.children[i]))
    return longest_word+1        


def averagelength(T):
    """ average word length

    """
    word_count = 0
    temp_length = 0
    for i in range (T.children):
            (lettre,isword) = i.key
            if(isword==True):
                word_count+=1
                temp_length += i
    return temp_length/word_count
    
###############################################################################
## search and list

def __wordlist(T,L,temp_str):
        for C in T.children:
            lettre,is_word = C.key
            word = temp_str + is_word
            if(is_word==True):
                L.append(word)
            __wordlist(C,L,word)


def wordlist(T):
    """ generate the word list
    
    """
    L = []
    __wordlist(T,L,"")
    return L    


def searchword(T, word):
    """ search for a word in a tree
    
    """
    L = wordlist(T)
    i = 0
    lenght = L.count
    is_present = False
    while (is_present and(i<lenght)):
        if(L[i]==word):
            is_present = True
        i+=1
    return is_present
    
def completion(T, prefix):
    """ generate the list of words with a common prefix
    
    """
    answerbool,myT = False,T
    length = len(prefix)
    for i in range (length):
        is_present = False
        t = 0
        while t < T.nbchildren and not is_present:
            if T.children[t].key[0] == prefix[i]:
                is_present = True
                T = T.children[t]
            t += 1
        if not is_present:
            answerbool,myT = False,T
    answerbool,myT = True,T
    
    
    if not answerbool:
        return []
    L = []
    decompt = 0
    if myT.key[1] == True:
        L.append(prefix)
        decompt = 1
    L += wordlist(myT)
    for i in range (decompt, len(L)):
        L[i] = prefix + L[i]
    return L
    


###############################################################################
## Build

def treetofile(T, filename):
    """ save the tree in a file
    
    """
    filesaving = open(filename,"treetofile")
    L = wordlist(T)
    for C in L:
        filesaving.write(C+'\n')
    filesaving.close()


def addword(T, word):
    """ add a word in the tree
    
    """
    

def treefromfile(filename):
    """ build the prefix tree from a file of words
    
    """
    filereader = open(filename,"treefromfile")
    T = tree.Tree(('',False),[])
    for C in filereader:
        addword(T,C.strip())
    return T

