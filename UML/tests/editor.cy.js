Cypress.on('uncaught:exception', (err, runnable) => {
  return false;
});


describe("minimal", () => {
  let myDiagram = null
  let myRobot = null

  beforeEach(() => {

    cy.visit("http://localhost:5238/Editor")

    // make sure the HTMLDivElement exists holding the Diagram
    cy.get("#myDiagramDiv")

    // load extensions/Robot.js dynamically
    cy.window().then(win => {
      const scr = win.document.createElement("script");
      scr.src = "https://unpkg.com/gojs/extensions/Robot.js";
      win.document.body.appendChild(scr);
    })

    // make sure it's loaded
    cy.window().should("have.property", "Robot")

    // save these references for each test, which simplifies each test code
    cy.window().then(win => {
      myDiagram = win.go.Diagram.fromDiv(win.document.getElementById("myDiagramDiv"));
      myRobot = new win.Robot(myDiagram);
    })
    // wait for the Diagram to finish initializing; is there a better way?
    cy.wait(1)
  })

  // A minimal test to make sure the Diagram got set up OK.
  it("Open with Zero Nodes", () => {
    cy.window().then(win => {
      expect(myDiagram.nodes.count).to.equal(0)

    })
  })

    // A minimal test to make sure the Diagram got set up OK.
  it("Create a node", () => {
    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.window().then(win => {
    expect(myDiagram.nodes.count).to.equal(1)
    })
  })
  
  it("Delete Node", () => {

    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.window().then(win => {
      var target = myDiagram.findNodeForKey("-1");
      if (target === null) return;
      var loc = target.location;
  
      // right click on target
      myRobot.mouseDown(loc.x + 10, loc.y + 10, 0, { right: true });
      myRobot.mouseUp(loc.x + 10, loc.y + 10, 100, { right: true });
  
      // move mouse over first context menu button
      myRobot.mouseMove(loc.x + 20, loc.y + 20, 200);
      // and click that button
      myRobot.mouseDown(loc.x + 20, loc.y + 20, 300);
      myRobot.mouseUp(loc.x + 20, loc.y + 20, 350);
      // This should have invoked the ContextMenuButton's click function, delete,
    expect(myDiagram.nodes.count).to.equal(0)
    })
  })

  it("Change Color", () => {

    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.window().then(win => {
      var target = myDiagram.findNodeForKey("-1");
      if (target === null) return;
      var loc = target.location;
  
      // right click on target
      myRobot.mouseDown(loc.x + 10, loc.y + 10, 0, { right: true });
      myRobot.mouseUp(loc.x + 10, loc.y + 10, 100, { right: true });
  
      // move mouse over second context menu button
      myRobot.mouseMove(loc.x + 20, loc.y + 40, 500);
      // and click that button
      myRobot.mouseDown(loc.x + 20, loc.y + 40, 600);
      myRobot.mouseUp(loc.x + 20, loc.y + 40, 650);
      // This should have invoked the ContextMenuButton's click function, change color,
    expect(target.data.color).to.equal('#ff0000')
    })
  })

  it("Save the Diagram", () => {
    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.get(':nth-child(4) > .nav-link').click()
    cy.window().then(win => {
    cy.get('textarea').should(($input) => {
      const value = $input.val();
        
      expect(value).to.not.equal('{ \"class\": \"go.GraphLinksModel\",');
        
    })
    })
  })

  it("Open a Diagram", () => {
    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.get(':nth-child(4) > .nav-link').click()
    cy.get(':nth-child(5) > .nav-link').click()
    cy.window().then(win => {
      var target = myDiagram.findNodeForKey("-1");
      expect(target.data.key).to.equal(-1);
    })
  })

  it("Create Link", () => {

    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.get('#toolbar > :nth-child(2) > .nav-link').click()
    cy.window().then(win => {
      var target1 = myDiagram.findNodeForKey("-1");
      var loc = target1.location;

      let options = {};
      myRobot.mouseDown(loc.x, loc.y, 0, options);
      myRobot.mouseMove(loc.x + 80, loc.y + 50, 50, options);
      myRobot.mouseMove(loc.x + 20, loc.y + 100, 100, options);
      myRobot.mouseUp(loc.x + 20, loc.y + 100, 150, options);

    expect(myDiagram.nodes.count).to.equal(2);
    expect(myDiagram.model.linkDataArray).to.not.equal([]);
    })
  })


})