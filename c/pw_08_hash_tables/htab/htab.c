#include <err.h>
#include <string.h>

#include "htab.h"

uint32_t hash(char *key)
{
    // TODO
}

struct htab *htab_new()
{
    // TODO
}

void htab_clear(struct htab *ht)
{
    // TODO
}

void htab_free(struct htab *ht)
{
    // TODO
}

struct pair *htab_get(struct htab *ht, char *key)
{
    // TODO
}

int htab_insert(struct htab *ht, char *key, void *value)
{
    // TODO
}

void htab_remove(struct htab *ht, char *key)
{
    // TODO
}
