# Forget Me Not

This README file contains a couple of reminders about the how to test
your project and main expected features of your handout.

## Testing Your Code


### Putting `opam` into action

`opam` is the `OCaml` package manager. You shall use it to install
all needed dependencies for the project. You first need to initialize
`opam` going through the different steps the following command takes
you in

  `opam init`

In order to have the different `opam` instructions available within
your shell you'll need a corresponding variable to add it to your
`.bashrc` through

  `eval $(opam env)`

If you want to make `opam` permanently available wihtin your
`bash` shell you'll need to add previous line to your `.bashrc` or
`.bash_profile` configurations files. To properly use your `AFS`
you're invited to look into `CRI Documentation
<https://rtfm.cri.epita.fr>`_.

### Installing `OASIS`


The easiest way to install OASIS is through your `OCaml` package
manager `opam` using command

  `opam install oasis`

`opam` is already installed on the image you have access to on the
school's machines. You're invited to use previous command line as well
the ones available hereby to install needed dependencies.


The configuration file for AFIT project uses an extra dependency that
is not provided within the available image : `ocamlfind`. It is
needed for the directives `FindLibName` and `FindLibParent`
defining the project hierarchy. It has to be installed through the
command

     `opam install ocamlfind`

### Compiling Your Project


You're having an `_oasis` file at the root of your repository. This
is the configuration file meant to set up your project's
build. Setting up your `OASIS` project is done using commad

  `oasis setup -setup-update dynamic`

This creates the files : `setup.ml`, `configure` and
`Makefile`. You better not meddle with any of them. These are the
needed tests to compile your project using the command

  `make`

from within the root of your project repository.

Notice that you raise an error if you try running a `make` at this
stage: the files tagged for compilation contain a specific dependency
called `oUnit`. You'll need to install it first as explained in the
next section.

### Testing Your Project

#### From The Shell

Each repository comes with a number of unit tests included within the
`tests` folders of each one of the project's phases. These are built
on using the `oUnit` library provided for `ocaml`. It is does not
come with the standard `ocaml` installation and has to be installed
through

  `opam install ounit`

You are welcome to add your own tests there if you understand what you
are doing. To run these tests you'll need to configure your
`setup.ml` file using the command

  ocaml setup.ml -configure --enable-tests

You can then run your tests at compilation with the command

  make test

This shall run the test suite for the whole project within the
terminal and specify possible errors and expected outputs in such a
case. Tests that went through do not give any specific messages except
for a `point` and a possible `OK`.

#### From Within OCaml Top-Level

You can test your functions from within `ocaml top-level` while
being in `emacs` `tuareg-mode` .

While being in the `builtin` directory you can evaluate each one of
the expressions of the `builtin.ml` file in the `ocaml top-level`
interpreter by using the `C-c C-e` shortcut. In order to evaluate
the whole current buffer you can use the `C-c C-b` shortcut. To
evaluate the `builtin.ml` file without necessarily opening it you
can use the `top-level` directive

  `#use "builtin.ml"`

All available function within the `builtin.ml` file are then
available within `top-level`.

The previous strategy is going to raise an error if you try evaluating
the file `basic_arithmetics.ml`. You'll get an error specifying that
the `Builtin` module is not bounded. This error says the
`top-level` environment cannot see any `Builtin` module to open ;
the `basic_arithmetics.ml` file starts by openning that module
(which corresponds to file `builtin.ml`). For `top-level` to
evaluate the `open Builtin` instruction you need to load
`builtin.ml` as a module in `top-level` using the directive

  `#mod_use "builtin.ml"`

You can then evaluate `basic_arithmetics.ml` using the directive

  `#use "basic_arithmetics.ml"`

In order to evaluate any given file `file.ml` in `top-level`
you'll generally need to load the modules it opens through `open`
instructions using `#mod_use` directive. You're invited to ask your
lab assistants for help if needed.

### Making Your Configuration Persistant

The list of installations we've exposed previously will not be
persistant from one session to another ; not even from a shell session
to another one. Here are the steps to follow in order to ensure you'll
find your system ready for compiling and testing purposes each time
you log on the PIE.

- Add a folder `opam` in `~/afs/.confs/` ;
- Add `opam` in the `dot_list` of `~/afs/.confs/install.sh`
- Add `command -V opam &> /dev/null && eval $(opam env)` in `~/.bashrc`.

Your welcome to ask for help during tutoring sessions regarding your
PIE session's configuration.

## Reminder

If you do not understand what you're doing **DO NOT** modify your
`_oasis` file without backing it up. Such a change might compromise
your project compilation process.

## Handout

Your handout is your project git repository. Any push to your git
repository modifies the state of your handout.

You are expected to respect your git repository structure. **No files
nor directories** are to be added to that structure anywhere else but
within the `Source/multithreading` directory, if relevant.

You should not need to add any functions to the pre-existing ones in
your project's repository. Nonetheless, you're allowed to as long as
the functions' your repository comes with are present in the
handout. In such a case these are auxiliary functions you use to
factor your code ; no other functions but the pre-existing ones are
going to be tested!

At initial state your repository is a buildable OASIS project. Your
handout should satisfy this property. Any file not going through
compilation process will not be included in the automatic grading
process.

Each student is expected to have his / her own handout. This is not a
group project!

Handouts will be collected by June 09 at 23:42. No other handouts
will be taken into account after this date.
