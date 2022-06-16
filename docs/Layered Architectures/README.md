# 1)Name examples of the layered architecture. Do they differ or just extend each other?
* N-Layer
* Onion
* Hexagonal
* Clean

Layered Architecture is such generic term. Nowadays, anything is layered architecture. Difference is what each layer does and how they depend on each other.

N-Layer architecture consists of layers. On to bottom is database /persistence layer, where data is persisted. On top is business layer, where all the interesting domain logic happens. And on top of that is presentation layer. The most important thing to notice is that business layer depends on persistence layer. This means that we cannot use business objects without also dragging in persistence. Usually in form of database.

Hexagonal, Clean and Onion architectures also have layers. But the difference between these and the layered architecture is that it puts the business domain at the bottom/center. This means that business model and domain entities are independent of their persistence. This is really helpful for testing, as you don't need to bother to spin up the database.

# 2) Is the below layered architecture correct and why? Is it possible from C to use B? from A to use C?
No. In a layered architecture, the lower layers should not depend on the higher ones. As we can see in the picture, the lower layer `C` has a dependency on the upper layer `B`, which violates the principle of layered architecture and looks more like a monolithic

# 3) Is DDD a type of layered architecture? What is Anemic model? Is it really an antipattern?
Domain-Driven Design is an approach to software development that centers the development on programming a domain model that has a rich understanding of the processes and rules of a domain. A particularly important part of DDD is the notion of Strategic Design - how to organize large domains into a network of Bounded Contexts. 

Being an architecture that encourages our domain to be the core of all the layers, and not to be coupled to anything external, they work perfectly together. We could say that DDD is based on a clean architecture as the central pillar in terms of architecture.

The basic symptom of an Anemic Domain Model is that at first blush it looks like the real thing. There are objects, many named after the nouns in the domain space, and these objects are connected with the rich relationships and structure that true domain models have.

# 4) What are architectural anti-patterns? Discuss at least three, think of any on your current or previous projects.
AntiPatterns, like their design pattern counterparts, define an industry vocabulary for the common defective processes and implementations within organizations. A higher-level vocabulary simplifies communication between software practitioners and enables concise description of higher-level concepts.

An AntiPattern is a literary form that describes a commonly occurring solution to a problem that generates decidedly negative consequences. The AntiPattern may be the result of a manager or developer not knowing any better, not having sufficient knowledge or experience in solving a particular type of problem, or having applied a perfectly good pattern in the wrong context.

# 5) What do Testability, Extensibility and Scalability NFRs mean. How would you ensure you reached them? Does Clean Architecture cover these NFRs?
* **Testability**. Each layer can be tested separately. For example, in Clean Architecture the business rules can be tested without the UI, Database, Web Server, or any other external element.
* **Extensibility**. It is the ability of the software system to allow and accept the significant extension of its capabilities without major rewriting of code or changes in its basic architecture. For example, in Clean Architecture the architecture does not depend on the existence of some library of feature-laden software. This allows us to use such frameworks as tools, rather than having to cram our system into their limited constraints.
* **Scalability**. It is the measure of a system’s ability to increase or decrease in performance and cost in response to changes in application and system processing demands. Each layer in a clean architecture can be scaled horizontally. If our project or team becomes too big, each service may become an independent "micro-service".

Clean architecture covers the above NFRs. Example for each requirement is also provided.