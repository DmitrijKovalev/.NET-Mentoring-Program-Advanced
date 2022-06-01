# 1) What are the cons and pros of the Monolith architectural style?
### Pros
* Simplicity of development. The monolithic approach is a standard way of building applications. No additional knowledge is required. All source code is located in one place which can be quickly understood.
* Simplicity of debugging. The debugging process is simple because all code is located in one place. We can easily follow the flow of a request and find an issue.
* Simplicity of testing. We test only one service without any dependencies. Everything is usually clear.
* Simplicity of deployment. Only one deployment unit should be deployed. There are no dependencies. In cases when UI is packaged with backend code we do not have any breaking changes. Everything exists and changes in one place.

### Cons
* This simple approach has a limitation in size and complexity.
* The size of the application can slow down the start-up time.
* A large single code base can be significantly harder to understand
* Tightly coupled components so difficult to change to a new or advanced technology, language, or framework. Since changes in frameworks or languages will affect an entire application it is extremely expensive in both time and cost.
* Must redeploy the entire application on each update.
* Monolithic applications can also be difficult to scale when different modules have conflicting resource requirements.

# 2) What are the cons and pros of the Microservices architectural style?
### Pros
* Improved fault isolation: Larger applications can remain mostly unaffected by the failure of a single module.
* Eliminate vendor or technology lock-in: Microservices provide the flexibility to try out a new technology stack on an individual service as needed. There won’t be as many dependency concerns and rolling back changes becomes much easier. With less code in play, there is more flexibility.
* Ease of understanding: With added simplicity, developers can better understand the functionality of a service.
* Smaller and faster deployments: Smaller codebases and scope = quicker deployments, which also allow us to start to explore the benefits of Continuous Deployment.
* Scalability: Since our services are separate, we can more easily scale the most needed ones at the appropriate times, as opposed to the whole application. When done correctly, this can impact cost savings.

### Cons
* Increased Complexity of Communication Between the Services.
* Global testing is difficult: Testing a microservices-based application can be cumbersome. In a monolithic approach, we would just need to launch our WAR on an application server and ensure its connectivity with the underlying database. With microservices, each dependent service needs to be confirmed before testing can occur.
* Debugging problems can be harder: Each service has its own set of logs to go through. Log, logs, and more logs.
* Deployment challengers: The product may need coordination among multiple services, which may not be as straightforward as deploying a WAR in a container.
* Large vs small product companies: Microservices are great for large companies, but can be slower to implement and too complicated for small companies who need to create and iterate quickly, and don’t want to get bogged down in complex orchestration.

# 3) What is the difference between SOA and Microservices?
* **Communication**: In a microservices architecture, each service is developed independently, with its own communication protocol. With SOA, each service must share a common communication mechanism called an enterprise service bus (ESB). SOA manages and coordinates the services it delivers through the ESB. However, the ESB can become a single point of failure for the whole enterprise, and if a single service slows down, the entire system can be affected.
* **Interoperability**: In the interest of keeping things simple, microservices use lightweight messaging protocols like HTTP/REST (Representational State Transfers) and JMS (Java Messaging Service). SOAs are more open to heterogeneous messaging protocols such as SOAP (Simple Object Access Protocol), AMQP (Advanced Messaging Queuing Protocol) and MSMQ (Microsoft Messaging Queuing).
* **Service granularity**: Microservices architectures are made up of highly specialized services, each of which is designed to do one thing very well. The services that make up SOAs, on the other hand, can range from small, specialized services to enterprise-wide services.
* **Speed**: By leveraging the advantages of sharing a common architecture, SOAs simplify development and troubleshooting. However, this also tends to make SOAs operate more slowly than microservices architectures, which minimize sharing in favor of duplication.
* **Governance**: The nature of SOA, involving shared resources, enable the implementation of common data governance standards across all services. The independent nature of microservices does not enable consistent data governance. This provides greater flexibility for each service, which can encourage greater collaboration across the organization.
* **Storage**: SOA and microservices also differ in terms of how storage resources are allocated. SOA architecture typically includes a single data storage layer shared by all services within a given application, whereas microservices will dedicate a server or database for data storage for any service that needs it.

# 4) What does hybrid architectural style mean? Think of your current and previous projects and try to describe which architectural styles they most likely followed.
Hybrid architectural style is combination architectural styles (Application Landscape, Application Structure, User Interface). This approach helps to solve complex problems.
As a rule, large enterprise products have a hybrid architectural style. That allows businesses to be more flexible and improve their apps and technologies.
The architecture style of my current project would be explained as hybrid. This is a combination of a Monolithic, Service-Oriented and and N-tier architecture

# 5) Name several examples of the distributed architectures. What do `ACID` and `BASE` terms mean?
### Examples of the distributed architectures
* Client-Server Architecture
* N-tier Architecture
* Broker Architectural Style
* Service-Oriented Architecture
* Microservices architectural style

The CAP theorem states that it is impossible to achieve both consistency and availability in a partition tolerant distributed system (i.e., a system which continues to work in cases of temporary communication breakdowns).

The fundamental difference between `ACID` and `BASE` database models is the way they deal with this limitation.

The `ACID` model provides a consistent system.
The BASE model provides high availability.

### `ACID` Model
The `ACID` database transaction model ensures that a performed transaction is always consistent.
`ACID` stands for:

* **Atomic** – Each transaction is either properly carried out or the process halts and the database reverts back to the state before the transaction started. This ensures that all data in the database is valid.
* **Consistent** – A processed transaction will never endanger the structural integrity of the database.
* **Isolated** – Transactions cannot compromise the integrity of other transactions by interacting with them while they are still in progress.
* **Durable** – The data related to the completed transaction will persist even in the cases of network or power outages. If a transaction fails, it will not impact the manipulated data.

### The `BASE` Model
`BASE` stands for:

* **Basically Available** – Rather than enforcing immediate consistency, BASE-modelled NoSQL databases will ensure availability of data by spreading and replicating it across the nodes of the database cluster.
* **Soft State** – Due to the lack of immediate consistency, data values may change over time. The BASE model breaks off with the concept of a database which enforces its own consistency, delegating that responsibility to developers.
* **Eventually Consistent** – The fact that BASE does not enforce immediate consistency does not mean that it never achieves it. However, until it does, data reads are still possible (even though they might not reflect the reality).

# 6) Name several use cases where Serverless architecture would be beneficial.
* Run something infrequently (example: once a day or once a month)
* Trigger-based tasks
Any user activity that triggers an event or a series of events is a good candidate for serverless architecture. For instance, a user signing up on your website may trigger a database change, which may, in turn, trigger a welcome email. The backend work can be handled through a chain of serverless functions.
* Building RESTful APIs
We can leverage Amazon API Gateway with serverless functions to build RESTful APIs that scale with demand.
* Asynchronous processing
Serverless functions can handle behind-the-scenes application tasks, such as rendering product information or transcoding videos after upload, without interrupting the flow of the application or adding user-facing latency.
* Security checks
When we spin up a new container, a function can be invoked to scan the instance for misconfigurations or vulnerabilities. Functions can also be used as a more secure option for SSH verification and two-factor authentication.
* Continuous Integration (CI) and Continuous Delivery (CD)
Serverless architectures can automate many of the stages in our CI/CD pipelines. For example, code commits can trigger a function to create a build, and pull requests can trigger automated tests.
